using E_Commerce.Core.Models;

namespace E_Commerce.Core.Specifications
{
    public class ProductWithFilterationForCountSpecification : BaseSpecifications<Product>
    {
        public ProductWithFilterationForCountSpecification(SpecsParameter Specs)
        : base(p =>
        (!Specs.BrandId.HasValue || p.BrandId == Specs.BrandId.Value)
        &&
        (!Specs.TypeId.HasValue || p.TypeId == Specs.TypeId.Value)
        )
        {

        }

    }
}