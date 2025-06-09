using Domain;
using Domain.Contracts;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Services.Specification;
using Shared;
using Stripe;
using Order = Domain.Entities.OrderEntities.Order;
using Product = Domain.Product;


namespace Services
{
    public class PaymentService: IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IConfiguration _configuration;

        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration,IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository= basketRepository;
            _configuration=configuration;

           // _projectRepository = new Lazy<IGenericRepository<PaymentService, Guid>>(() => _unitOfWork.GetRepository<PaymentService, Guid>());
        }
        // 1] set up stripe Api Key [secreteKey] ==> from appsettings [configuration]
        // 2] Get the basket ==> to get the price of items in the basket so you can calculate the total
        // 3] basket.item=product.database ==> [update basket item price , get product price from data base]
        // 4] Get delivery method and its shipping price
        // 5] retrive deliverymethod from data base and assign price of basket [shipping address]= deliveryMethod.shippingPrice
        // 6] total = subtotal + shippingPrice ==> [(item.price)*(item.quantity)]+basket.shippingPrice
        // 7] create or update paymentintent with stripe
        // 8] save changes to the basket
        // 9] mapping basket ==> basketDto
        // 10] return
        public async Task<BasketDto> CreateOrUpdatePaymnetIntentAsync(string basketId)
        {
            // 1] set up stripe Api Key [secreteKey] ==> from appsettings [configuration]
            // configure stripe api key using secretKey from appsettings
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];

            // 2] Get the basket ==> to get the price of items in the basket so you can calculate the total
            //retrive basket using basketId
            var basket = await _basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);

            // 3] basket.item=product.database ==> [update basket item price , get product price from data base]
            foreach (var item in basket.Items)
            {
                // get the product price from database [validate the end user sent or the price correctly] 
                var product = await _unitOfWork.GetRepository<Product, Guid>().GetByIdAsync(item.ProductId)
                   ?? throw new ProductNotFoundException(item.ProductId);
                //update item.price in the basket [data from product table in data base]
                item.Price = product.price;

            }

            //// 4] Get delivery method and its shipping price
            ////check if the delivery method is selected or not
            //if (!basket.DeliveryMethodId.HasValue) throw new Exception("no delivery method was selected");
            //// retrive delivery method from db
            //var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, Guid>().GetByIdAsync(basket.DeliveryMethodId.Value) ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            //// 5] retrive deliverymethod from data base and assign price of basket [shipping address]= deliveryMethod.shippingPrice
            //// basket.shippingPrice = actual price from data base
            //basket.ShippingPrice = deliveryMethod.Price;

            basket.ShippingPrice = 50;

            // 6] total = subtotal + shippingPrice ==> [(item.price)*(item.quantity)]+basket.shippingPrice  =====> long
            // long ==> dollar ==> cent
            var amount = (long)(basket.Items.Sum(i => i.Price * i.Quantity) + basket.ShippingPrice) * 100;

            // 7] create or update paymentintent with stripe
            var service = new PaymentIntentService();
            // if u want to create or update 
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // create
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                //call stripe api from service [create payment intent]
                var paymentIntent = await service.CreateAsync(createOptions); //Id , ClientSecret

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // update 
                // 1] product price is changed [admin] [data base]
                // 2] user change deliveryMethod
                // 3] user remove items from basket , add items ...
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount  //--> salary
                };
                await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }

            // 8] save changes to the basket
            await _basketRepository.UpdateBasketAsync(basket); // items'Price , shippingPrice


            // 9] mapping basket ==> basketDto
            // 10] return
            return MapToDto(basket);



        }
        // Manual Mapping: Basket -> BasketDto
        private static BasketDto MapToDto(Basket basket) => new BasketDto
        {
            Username = basket.Username,
            Items = basket.Items.Select(item => new BasketItemDto
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        };

        // Manual Mapping: BasketDto -> Basket
        private static Basket MapToEntity(BasketDto dto) => new Basket
        {
            Username = dto.Username,
            Items = dto.Items.Select(item => new BasketItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        };

        public async Task UpdatePaymnetStatus(string request, string stripeHeaders)
        {
            var endPointSecret = _configuration.GetSection("StripeSettings")["EndPointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(request,
                stripeHeaders, endPointSecret, throwOnApiVersionMismatch: false);
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            // Handle the event
            // If on SDK version < 46, use class Events instead of EventTypes
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentStatusSucceeded(paymentIntent.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentStatusFailed(paymentIntent.Id);

            }
            // ... handle other event types
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }



        }

        private async Task UpdatePaymentStatusFailed(string PaymentIntentid)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(PaymentIntentid)) ?? throw new Exception();
            order.PaymentStatus = OrderPaymentStatus.PaymentFaild;
            orderRepo.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentStatusSucceeded(string PaymentIntentid)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(PaymentIntentid)) ?? throw new Exception();
            order.PaymentStatus = OrderPaymentStatus.PaymentReceived;
            orderRepo.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

    }

}
