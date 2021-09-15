using AutoMapper;
using Shop.API.Helpers;

namespace Shop.API.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Entities.Product, Models.ProductDto>()
                .ForMember(
                dest => dest.purchasedDate,
                opt => opt.MapFrom(src => src.purchasedDate.GetPurchasedDate()));

            CreateMap<Models.ProductForCreationDto, Entities.Product>();

            CreateMap<Models.ProductForUpdateDto, Entities.Product>();

            CreateMap<Entities.Product, Models.ProductForUpdateDto>();
        }
    }
}
