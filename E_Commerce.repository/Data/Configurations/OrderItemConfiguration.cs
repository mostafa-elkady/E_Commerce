using E_Commerce.Core.Models.Order_Aggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.repository.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Data Type Of Decimal Column
            builder.Property(O => O.Price).HasColumnType("decimal(18,2)");
            // ProductItem [Not Table] => OrderItem + ProductItem => One Table
            builder.OwnsOne(O => O.Product, P => P.WithOwner());
        }
    }
}
