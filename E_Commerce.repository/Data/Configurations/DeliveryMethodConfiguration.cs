using E_Commerce.Core.Models.Order_Aggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.repository.Data.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            // Data Type Of Decimal Column
            builder.Property(d => d.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
