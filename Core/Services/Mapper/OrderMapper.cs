using Domain;
using Domain.Entities.OrderEntities;
using Shared;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapper
{
    public class OrderMapper
    {
        public static AddressDto MapToAddressDTO(Address address)
        {
            return new AddressDto
            {
                city = address.city,
                street = address.street,
                region = address.region,

            };
        }

        public static Address MapToAddress(AddressDto dto)
        {
            return new Address
            {
                city = dto.city,
                street = dto.street,
                region = dto.region,
            };
        }

        public static DeliveryMethodResult MapToDeliveryMethodResult(DeliveryMethod method)
        {
            return new DeliveryMethodResult
            {
                Id=method.Id,
                ShortName = method.ShortName,
                Description = method.Description,
                Price = method.Price,
                DeliveryTime = method.DeliveryTime,               
            };
        }

        public static OrderItemDTO MapToOrderItemDTO(OrderItem item)
        {
            return new OrderItemDTO
            {
                ProductId = item.Product.ProductId,
                ProductName = item.Product.ProductName,
                ImageUrl = item.Product.ImageUrl,
                Quantity = item.Quantity,
                Price = item.Price
            };

        }

        public static OrderResult MapToOrderResult(Order order)
        {
            return new OrderResult
            {
                Id = order.Id,
                UserEmail = order.UserEmail,
                OrderDate = order.OrderDate,
                ShippingAddress = MapToAddressDTO(order.ShippingAddress),
                DeliveryMethod = order.DeliveryMethod.ShortName,
                orderItems = order.orderItems.Select(MapToOrderItemDTO).ToList(),
                SubTotal = order.SubTotal,
                Total = order.SubTotal + order.DeliveryMethod.Price,
                PaymentStatus = order.PaymentStatus.ToString(),
                PaymentIntentId= order.PaymentIntentId,
            };
        }
    }
}
