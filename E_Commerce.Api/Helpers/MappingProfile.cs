using AutoMapper;
using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.OrderDto;
using E_Commerce.Core.Models;
using E_Commerce.Core.Models.Order_Aggregation;

namespace E_Commerce.Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.productBrand, o => o.MapFrom(s => s.productBrand.Name))
                .ForMember(d => d.productType, o => o.MapFrom(s => s.productType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerCart, CustomerCartDto>().ReverseMap();

            CreateMap<CartItem, CartItemDto>().ReverseMap();

            //Order Mapping
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>()).ReverseMap();
            CreateMap<Order, OrderResultDto>()
                .ForMember(d => d.OrderDate, o => o.MapFrom(s => s.OrderDate))
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Cost))
                .ReverseMap()
                .ForMember(s => s.OrderDate, o => o.MapFrom(d => d.OrderDate));
        }
    }
}
