using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record DeliveryMethodResult
    {
        public Guid Id { get; init; }
        public string ShortName { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public string DeliveryTime { get; init; } //within a day

    }
}
