using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(_ => _.ProductType, _ => _.MapFrom(_ => _.ProductType.Name))
                .ForMember(_ => _.ProductBrand, _ => _.MapFrom(_ => _.ProductBrand.Name))
                .ForMember(_ => _.ImageUrl, _ => _.MapFrom<ProductUrlResolver>());
        }
    }
}
