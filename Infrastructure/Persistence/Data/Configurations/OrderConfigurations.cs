using Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, s => s.WithOwner());
           
            builder.HasMany(o => o.orderItems).WithOne();

            // علشان لو غيرت في السترينج اللي مكتوب في ال enum [يعني كتبت paymentsucceed بدل paymnetreceived]
            builder.Property(o => o.PaymentStatus).HasConversion(paymentstauts => paymentstauts.ToString(),  //  --> means paymentStatus will be stored as string in database
              s=> Enum.Parse<OrderPaymentStatus>(s)); //--> the type of returned value [Enum]

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany(dm=>dm.Orders)
                .HasForeignKey(o => o.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);
           
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,3)");
            
        }
    }
}
