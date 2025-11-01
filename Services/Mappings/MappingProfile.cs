using AutoMapper;
using Repositories.DTOs;
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

            CreateMap<CreateProductModel, Product>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()));

            CreateMap<CreateProductImageModel, ProductImage>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()));

            CreateMap<CreateProductVariantModel, ProductVariant>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()));

            CreateMap<UpdateProductModel, Product>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()));

            CreateMap<UpdateProductImageModel, ProductImage>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()));

            CreateMap<UpdateProductModel, UpdateProductDAO>()
                .ReverseMap();

            CreateMap<UpdateProductImageModel, UpdateProductImageDAO>()
                .ReverseMap();

            CreateMap<DTOs.AdminOrderFilterRequest, Repositories.DTOs.AdminOrderFilterRequest>();
            CreateMap<DTOs.UserOrderFilterRequest, Repositories.DTOs.UserOrderFilterRequest>();
            CreateMap<DTOs.ProductFilterRequest, Repositories.DTOs.ProductFilterRequest>();

        }
    }
}
