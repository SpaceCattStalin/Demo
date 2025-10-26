using AutoMapper;
using Repositories.Entities;
using Services.DTOs;

namespace Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product → ProductThumbnailDto
            CreateMap<Product, ProductThumbnailDto>()
                .ForMember(dest => dest.ThumbnailUrl,
                           opt => opt.MapFrom(src =>
                               src.Images.FirstOrDefault(i => i.IsPrimary).Url));

            // Product → ProductDto
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));

            // ProductVariant → ProductVariantDto
            CreateMap<ProductVariant, ProductVariantDto>();

            // ProductImage → ProductImageDto
            CreateMap<ProductImage, ProductImageDto>()
                .ForMember(dest => dest.ImageTypeCode,
                           opt => opt.MapFrom(src => src.ImageType.Code));
        }
    }
}
