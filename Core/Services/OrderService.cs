using Domain;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Persistence.Repositories;
using Services.Abstraction;
using Services.Mapper;
using Services.Specification;
using Shared.OrderModels;
using System.Dynamic;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IGenericRepository<Order, Guid>> _orderRepository;
        private readonly IBasketRepository _basketRepository;
        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = new Lazy<IGenericRepository<Order, Guid>>(
                () => _unitOfWork.GetRepository<Order, Guid>());
            _basketRepository = basketRepository;
        }
        public async Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            // Address [shipping Address]
            var shippingAddress = OrderMapper.MapToAddress(request.ShippingAddress); // Address

            // orderItem ==> basket [basketId] ==> basketItem ==> orderitem
            var basket = await _basketRepository.GetBasketAsync(request.BasketId) ?? throw new BasketNotFoundException(request.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, Guid>().GetByIdAsync(item.ProductId)
                    ?? throw new ProductNotFoundException(item.ProductId);
                // علشان ال orderItem ==> complex من جوا هنكريت ميثود [CreateOrderItem]
                orderItems.Add(CreateOrderItem(item, product));
            }

           

            // SubTotal
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var existingOrder = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId));
            if (existingOrder != null)
            {
                orderRepo.Delete(existingOrder);
            }
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            var shippingPrice = 50;


            // create order
            var order =new Order(userEmail, shippingAddress, orderItems,subTotal,basket.PaymentIntentId);
            order.ShippingPrice = shippingPrice;

            // save db
            await orderRepo.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            // map , return
            return OrderMapper.MapToOrderResult(order);


        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
        => new OrderItem(new ProductInOrderItem(product.productId, product.name, product.imageUrl),
             item.Quantity, product.price);


        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order=await _unitOfWork.GetRepository<Order,Guid>().GetByIdAsync(new OrderWithIncludesSpecifications(id)) ?? throw new OrderNotFoundException(id);
            return OrderMapper.MapToOrderResult(order);
        }
        
        public async Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludesSpecifications(userEmail));
            return orders.Select(OrderMapper.MapToOrderResult);
        }




    }
}
