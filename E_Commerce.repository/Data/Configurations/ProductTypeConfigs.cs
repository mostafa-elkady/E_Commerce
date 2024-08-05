using E_Commerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.repository.Data.Configurations
{
    internal class ProductTypeConfigs : IEntityTypeConfiguration<ProductType>
    {
        void IEntityTypeConfiguration<ProductType>.Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(type => type.Name).IsRequired();

        }
    }
}
