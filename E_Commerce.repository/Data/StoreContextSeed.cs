using E_Commerce.Core.Models;
using E_Commerce.Core.Models.Order_Aggregation;
using System.Text.Json;

namespace E_Commerce.repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedDataAsync(StoreContext context)
        {
            //Brands Seeding
            if (!context.ProductBrands.Any())
            {
                // to Read Json File
                var BrandsData = File.ReadAllText("../E_Commerce.Repository/Data/SeedData/brands.json");
                // to convert Json file to List
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                // to add List in dataBase
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await context.Set<ProductBrand>().AddAsync(Brand);
                    }
                    await context.SaveChangesAsync();
                }
            }

            //Types Seeding
            if (!context.ProductTypes.Any())
            {
                // to Read Json File
                var TypesData = File.ReadAllText("../E_Commerce.Repository/Data/SeedData/types.json");
                // to convert Json file to List
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                // to add List in dataBase
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await context.Set<ProductType>().AddAsync(Type);
                    }
                    await context.SaveChangesAsync();
                }
            }

            //Product Seeding
            if (!context.Products.Any())
            {
                // to Read Json File
                var ProductData = File.ReadAllText("../E_Commerce.Repository/Data/SeedData/products.json");
                // to convert Json file to List
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                // to add List in dataBase
                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                    {
                        await context.Set<Product>().AddAsync(Product);
                    }
                    await context.SaveChangesAsync();
                }
            }

            //DeliveryMethods Seeding
            if (!context.DeliveryMethods.Any())
            {
                // to Read Json File
                var DeliveryMethodsData = File.ReadAllText("../E_Commerce.Repository/Data/SeedData/delivery.json");
                // to convert Json file to List
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                // to add List in dataBase
                if (DeliveryMethods?.Count > 0)
                {
                    foreach (var DeliveryMethod in DeliveryMethods)
                    {
                        await context.Set<DeliveryMethod>().AddAsync(DeliveryMethod);
                    }
                    await context.SaveChangesAsync();
                }
            }

        }
    }
}
