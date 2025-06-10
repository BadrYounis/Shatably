using Domain.Contracts;
using Domain;
using Services.Abstraction;
using Shared;
using Domain.Exceptions;
using Stripe;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IWorkerService _workerService;
        private readonly IServicesWorkService _servicesWorkService;
        private readonly IProjectPortfolioService _projectPortfolioService;
        private readonly Lazy<IGenericRepository<ServiceBooking, Guid>> _bookingRepository;
        public BookingService(IUnitOfWork unitOfWork, IConfiguration configuration, IServicesWorkService servicesWorkService,
            IProjectPortfolioService projectPortfolioService, IWorkerService workerService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _workerService = workerService;
            _servicesWorkService = servicesWorkService;
            _projectPortfolioService = projectPortfolioService;
            _bookingRepository = new Lazy<IGenericRepository<ServiceBooking, Guid>>(
                () => _unitOfWork.GetRepository<ServiceBooking, Guid>());
        }
        public async Task<BookingDto> CreateBookingAsync(BookingCreateDto dto)
        {
            if (dto.ClientId == null && dto.WorkerId == null && dto.PortfolioId == null && dto.ServiceId == null)
                throw new ArgumentException("At least one of ClientId, WorkerId, PortfolioId or ServiceId must be provided.");

            decimal totalPrice = 0;

            // Get service if selected
            if (dto.ServiceId.HasValue)
            {
                var service = await _servicesWorkService.GetByIdAsync(dto.ServiceId.Value)
                    ?? throw new NotFoundException($"Service with ID {dto.ServiceId} not found");
                totalPrice = service.Price;
            }

            // Get portfolio if selected
            else if (dto.PortfolioId.HasValue)
            {
                var portfolio = await _projectPortfolioService.GetByIdAsync(dto.PortfolioId.Value)
                    ?? throw new NotFoundException($"Portfolio with ID {dto.PortfolioId} not found");
                totalPrice = portfolio.Price;
            }

            // Get worker if selected
            else if (dto.WorkerId.HasValue)
            {
                var worker = await _workerService.GetByIdAsync(dto.WorkerId.Value)
                    ?? throw new NotFoundException($"Worker with ID {dto.WorkerId} not found");
                totalPrice = worker.Price;
            }

            var entity = new ServiceBooking
            {
                BookingId = Guid.NewGuid(),
                BookingDate = dto.BookingDate,
                TimeSlot = dto.TimeSlot,
                ClientId = dto.ClientId,
                WorkerId = dto.WorkerId,
                PortfolioId = dto.PortfolioId,
                ServiceId = dto.ServiceId,
                Status = "Pending",
                Latitude = dto.Location.Latitude,
                Longitude = dto.Location.Longitude,
                Address = dto.Location.Address,
                Description = dto.Location.Description,
                TotalPrice = totalPrice,
            };

            await _bookingRepository.Value.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(entity);
        }

        public async Task DeleteBookingAsync(Guid bookingId)
        {
            var booking = await _bookingRepository.Value.GetByIdAsync(bookingId);
            if (booking is null)
                throw new NotFoundException($"{bookingId} is not found");

            // Stripe refund logic
            if (!string.IsNullOrEmpty(booking.PaymentIntentId))
            {
                StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];

                var service = new RefundService();
                var refundOptions = new RefundCreateOptions
                {
                    PaymentIntent = booking.PaymentIntentId
                };

                try
                {
                    await service.CreateAsync(refundOptions);
                }
                catch (StripeException ex)
                {
                    // Handle or log Stripe refund failure
                    throw new ApplicationException($"Refund failed: {ex.Message}");
                }
            }

            _bookingRepository.Value.Delete(booking);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsForClientAsync(Guid clientId)
        {
            var repo = _unitOfWork.GetRepository<ServiceBooking, Guid>();
            var bookings = await repo.FindAsync(b => b.ClientId == clientId);
            return bookings.Select(MapToDto);
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsForWorkerAsync(Guid workerId)
        {
            var repo = _unitOfWork.GetRepository<ServiceBooking, Guid>();
            var bookings = await repo.FindAsync(b => b.WorkerId == workerId);
            return bookings.Select(MapToDto);
        }
        public async Task<BookingDto> GetBookingByIdAsync(Guid bookingId)
        {
            var repo = _unitOfWork.GetRepository<ServiceBooking, Guid>();
            var entity = await repo.GetByIdAsync(bookingId);
            return MapToDto(entity);
        }
        public async Task<BookingDto> UpdateBookingAsync(BookingUpdateDto dto)
        {
            var repo = _unitOfWork.GetRepository<ServiceBooking, Guid>();
            var entity = await _bookingRepository.Value.GetByIdAsync(dto.BookingId) ?? throw new Exception("Booking not found");

            entity.BookingDate = dto.BookingDate;
            entity.TimeSlot = dto.TimeSlot;
            entity.Status = dto.Status;
            entity.PortfolioId = dto.PortfolioId;
            entity.ServiceId = dto.ServiceId;
            entity.Latitude = dto.Location.Latitude;
            entity.Longitude = dto.Location.Longitude;

            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(entity);
        }
        private BookingDto MapToDto(ServiceBooking b) => new BookingDto
        {
            BookingId = b.BookingId,
            BookingDate = b.BookingDate,
            Status = b.Status,
            TimeSlot = b.TimeSlot,
            ClientId = b.ClientId,
            WorkerId = b.WorkerId,
            TotalPrice = b.TotalPrice,
            PortfolioId = b.PortfolioId,
            ServiceId = b.ServiceId,
            Location = new LocationDto
            {
                Latitude = b.Latitude,
                Longitude = b.Longitude,
                Address = b.Address,
                Description = b.Description,             
            }
        };
    }
}
