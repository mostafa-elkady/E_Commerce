using E_Commerce.Core.Models;

namespace E_Commerce.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {

        public ProductWithBrandAndTypeSpecifications(SpecsParameter specs)
            : base(p =>
            (!specs.TypeId.HasValue || p.TypeId == specs.TypeId.Value) &&
             (!specs.BrandId.HasValue || p.BrandId == specs.BrandId.Value) &&
            (string.IsNullOrWhiteSpace(specs.Search) || p.Name.ToLower().Contains(specs.Search))
            )
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
            ApplyPagination(specs.PageSize, specs.PageIndex);

            switch (specs.SortBy)
            {
                case SortOptions.NameAscending:
                    OrderBy = x => x.Name;
                    break;
                case SortOptions.NameDescending:
                    OrderByDesc = x => x.Name;
                    break;
                case SortOptions.PriceAscending:
                    OrderBy = x => x.Price;
                    break;
                case SortOptions.PriceDescending:
                    OrderByDesc = x => x.Price;
                    break;
                default:
                    OrderBy = x => x.Name;
                    break;
            }


        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
        }
    }
}
