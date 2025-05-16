using Domain;
using Domain.Contracts;
using Domain.Exceptions;
using Services.Abstraction;
using Shared;
using Shared.Dtos;

namespace Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IGenericRepository<Client, Guid>> _clientRepository;
        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _clientRepository = new Lazy<IGenericRepository<Client, Guid>>(
                () => _unitOfWork.GetRepository<Client, Guid>());
        }
        public async Task<IEnumerable<ClientResultDto>> GetAllAsync()
        {
            var clients = await _clientRepository.Value.GetAllAsync();
            return clients.Select(MapToDto);
        }
        public async Task<ClientResultDto> GetByIdAsync(Guid clientId)
        {
            var client = await _clientRepository.Value.GetByIdAsync(clientId);
            return client is null ? throw new ClientNotFoundException(clientId) : MapToDto(client);
        }
        public async Task<Guid> CreateAsync(CreateClientDto dto)
        {
            var client = new Client
            {
                ClientId = Guid.NewGuid(),
                fullName = dto.FullName,
                email = dto.Email,
                phoneNumber = dto.PhoneNumber,
                Address = new Address
                {
                    city = dto.Address.city,
                    region = dto.Address.region,
                    street = dto.Address.street
                }
            };
            await _clientRepository.Value.AddAsync(client);
            await _unitOfWork.SaveChangesAsync();
            return client.ClientId;
        }
        public async Task UpdateAsync(Guid clientId, UpdateClientDto dto)
        {
            var client = await _clientRepository.Value.GetByIdAsync(clientId);
            if (client is null)
                throw new ClientNotFoundException(clientId);
            client.email = dto.Email;
            client.phoneNumber = dto.PhoneNumber;
            client.fullName = dto.FullName;
            client.Address = new Address
            {
                city = dto.Address.city,
                region = dto.Address.region,
                street = dto.Address.street
            };
        }
        public async Task DeleteAsync(Guid clientId)
        {
            var client = await _clientRepository.Value.GetByIdAsync(clientId);
            if (client is null)
                throw new ClientNotFoundException(clientId);

            _clientRepository.Value.Delete(client);
            await _unitOfWork.SaveChangesAsync();
        }
        private static ClientResultDto MapToDto(Client client) => new ClientResultDto
        {
            ClientId = client.ClientId,
            fullName = client.fullName,
            phoneNumber = client.phoneNumber,
            email = client.email,
            Address = client.Address is null ? throw new NotFoundException("Address is required!") : new AddressDto
            {
                city = client.Address.city,
                region = client.Address.region,
                street = client.Address.street,
            },
        };
    }
}
