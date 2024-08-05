using E_Commerce.Core.Models.Order_Aggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.repository.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //  DB => string “Pending”   APP => OrderStatus.Pending
            builder.Property(O => O.Status)
                .HasConversion(OS => OS.ToString(), OS => (OrderStatus)Enum.Parse(typeof(OrderStatus), OS));

            // Data Type Of Decimal Column
            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");

            // Address [Not Table] => Order + Address => One Table
            builder.OwnsOne(O => O.ShippingAddress, X => X.WithOwner());
            // Dlivery Method
            builder.HasOne(O => O.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
