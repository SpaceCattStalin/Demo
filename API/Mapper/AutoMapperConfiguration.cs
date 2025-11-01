using API.Models;
using AutoMapper;
using Repositories.Basic;
using Repositories.Entities;
using Services.DTOs;

namespace API.Mapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Cart, CartModel>();
            CreateMap<Product, ProductModel>();
            CreateMap<Product, Services.DTOs.CreateProductModel>();
            CreateMap<Product, Services.DTOs.UpdateProductModel>();
            CreateMap<Order, OrderModel>();
            //CreateMap<Payment, PaymentModel>()
            //    .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Method.Name));
            CreateMap<Shipping, ShippingModel>();
            CreateMap<User, UserModel>();
            CreateMap<User, UpdateUserModel>().ReverseMap();

            // Category -> CategoryModel
            CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.ImgUrl,
                opt => opt.MapFrom(src => src.Image.Url));

            // Product -> ProductModel 
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Variants,
                opt => opt.MapFrom(src => src.Variants))
                .ForMember(dest => dest.Images,
                opt => opt.MapFrom(src => src.Images))
                .ReverseMap();

            // ProductVariant -> ProductVariantModel
            //CreateMap<ProductVariant, ProductVariantModel>().ReverseMap();
            CreateMap<ProductVariant, ProductVariantModel>()
                .ForMember(dest => dest.Sizes,
                    opt => opt.MapFrom(src => src.Sizes));

            // ProductImage -> ProductImageModel
            CreateMap<ProductImage, ProductImageModel>().ReverseMap();
            CreateMap<ProductSize, ProductSizeModel>()
                .ForMember(dest => dest.Size,
                opt => opt.MapFrom(src => src.Size.SizeType));

            // Size -> SizeDTO  
            CreateMap<SizeDTO, Size>().ReverseMap();

            // CartItem -> CartItemDTO
            CreateMap<CartItemDTO, CartItem>().ReverseMap()
                .ForMember(dest => dest.CartItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductVariantId, opt => opt.MapFrom(src => src.ProductVariantId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductVariant.Product.Name + " " + src.ProductVariant.Color))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.ProductVariant.Color))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Quantity * src.ProductVariant.Product.Price))
                .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => new CartItemSizeDTO
                {
                    SizeType = src.Size.SizeType,
                    StockQuantity = src.ProductVariant.Sizes.FirstOrDefault(ps => ps.SizeId == src.SizeId).StockQuantity
                }))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.ProductVariant.Images
                        .Where(i => i.IsPrimary == true)
                        .Select(i => i.Url)
                        .FirstOrDefault()));

            CreateMap<Shipping, ShippingModel>()
                .ForMember(dest => dest.ShippingId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeOffset
                    .FromUnixTimeSeconds(src.StartDate).ToLocalTime().DateTime))
                .ForMember(dest => dest.FinishDate, opt => opt.MapFrom(src => DateTimeOffset
                    .FromUnixTimeSeconds(src.FinishDate.Value).ToLocalTime().DateTime))
                .ReverseMap()
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(src => ((DateTimeOffset)src.StartDate).ToUnixTimeSeconds()))
                .ForMember(dest => dest.FinishDate,
                    opt => opt.MapFrom(src => ((DateTimeOffset)src.FinishDate).ToUnixTimeSeconds()));

            CreateMap<Payment, PaymentModel>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeOffset
                    .FromUnixTimeSeconds(src.CreatedDate).ToLocalTime().DateTime))
                .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Method.Name))
                .ReverseMap()
                .ForMember(dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => ((DateTimeOffset)src.CreatedDate).ToUnixTimeSeconds()));


            CreateMap<Order, OrderModel>()
                .ForMember(dest => dest.OrderId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTimeOffset
                        .FromUnixTimeSeconds(src.CreatedDate).ToLocalTime().DateTime))
                .ForMember(dest => dest.Payment,
                    opt => opt.MapFrom(src => src.Payments.FirstOrDefault()))
                .ForMember(dest => dest.Shipping,
                    opt => opt.MapFrom(src => src.Shippings.FirstOrDefault()))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ReverseMap()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => ((DateTimeOffset)src.CreatedDate).ToUnixTimeSeconds()))
                .ForMember(dest => dest.Payments,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Shippings,
                    opt => opt.Ignore());

            CreateMap<OrderItem, OrderItemDTO>()
                //.ForMember(dest => dest.SizeType, opt => opt.MapFrom(src => src.ProductVariant.Sizes.Where(ps => ps.ProductVariantId == src.ProductVariantId && ps.)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductVariant.Product.Name + " " + src.ProductVariant.Color));

            CreateMap<Order, AdminOrderModel>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.CreatedDate).ToLocalTime().DateTime))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Users.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Users.Name))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Users.Email))
                .ForMember(dest => dest.PaymentAmount, opt => opt.MapFrom(src => src.Payments.FirstOrDefault().Amount))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.Payments.FirstOrDefault().Status))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payments.FirstOrDefault().Method.Name))
                .ForMember(dest => dest.ShippingStatus, opt => opt.MapFrom(src => src.Shippings.FirstOrDefault().Status))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Shippings.FirstOrDefault().EndAddress));


            // ProductSize -> CartItemSizeDTO 
            //CreateMap<CartItemSizeDTO, ProductSize>().ReverseMap()
            //    .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
            //    .ForMember(dest => dest.SizeType, opt => opt.MapFrom(src => src.ProductVariant.S))

            CreateMap<Order, AdminOrderDetailModel>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.CreatedDate).ToLocalTime().DateTime))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Users.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Users.Name))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Users.Email))
                .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.Users.Phone))
                .ForMember(dest => dest.UserAddress, opt => opt.MapFrom(src => src.Users.Address))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payments.FirstOrDefault()))
                .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => src.Shippings.FirstOrDefault()))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<OrderItem, AdminOrderItemModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductVariant.Product.Name))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.ProductVariant.Color))
                .ForMember(dest => dest.SizeType, opt => opt.MapFrom(src =>
                    src.SizeType))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductVariant.Product.Price))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ProductVariant.Images
                    .Where(i => i.IsPrimary)
                    .Select(i => i.Url)
                    .FirstOrDefault()));

            CreateMap<Shipping, ShippingDetailDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Order.Users.Name))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Order.Users.Email))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Order.Users.Phone))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeOffset
                    .FromUnixTimeSeconds(src.StartDate).ToLocalTime().DateTime))
                .ForMember(dest => dest.FinishDate,
                    opt => opt.MapFrom(src => src.FinishDate.HasValue
                        ? DateTimeOffset.FromUnixTimeSeconds(src.FinishDate.Value).ToLocalTime().DateTime
                        : (DateTime?)null))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Order.Total))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Order.Items));

            CreateMap<OrderItem, ShippingOrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductVariant.Product.Name))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.ProductVariant.Color))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.SizeType));

            // USER Order Summary (List)
            CreateMap<Order, UserOrderSummaryModel>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.CreatedDate).DateTime))
                .ForMember(dest => dest.PaymentStatus,
                    opt => opt.MapFrom(src => src.Payments.FirstOrDefault().Status))
                .ForMember(dest => dest.ShippingStatus,
                    opt => opt.MapFrom(src => src.Shippings.FirstOrDefault().Status));

            // USER Order Detail
            CreateMap<Order, UserOrderDetailModel>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.CreatedDate).DateTime))
                .ForMember(dest => dest.UpdatedDate,
                    opt => opt.MapFrom(src => src.UpdatedDate.HasValue
                        ? DateTimeOffset.FromUnixTimeSeconds(src.UpdatedDate.Value).DateTime
                        : (DateTime?)null))
                .ForMember(dest => dest.PaymentMethod,
                    opt => opt.MapFrom(src => src.Payments.FirstOrDefault().Method.Name))
                .ForMember(dest => dest.PaymentStatus,
                    opt => opt.MapFrom(src => src.Payments.FirstOrDefault().Status))
                .ForMember(dest => dest.ShippingStatus,
                    opt => opt.MapFrom(src => src.Shippings.FirstOrDefault().Status))
                .ForMember(dest => dest.StartAddress,
                    opt => opt.MapFrom(src => src.Shippings.FirstOrDefault().StartAddress))
                .ForMember(dest => dest.EndAddress,
                    opt => opt.MapFrom(src => src.Shippings.FirstOrDefault().EndAddress))
                .ForMember(dest => dest.ExpectedDeliveryDate,
                    opt => opt.MapFrom(src => src.Shippings.FirstOrDefault().FinishDate.HasValue
                        ? DateTimeOffset.FromUnixTimeSeconds(src.Shippings.FirstOrDefault().FinishDate.Value).DateTime
                        : (DateTime?)null))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            // USER Order Item
            CreateMap<OrderItem, UserOrderItemModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductVariant.Product.Name))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.ProductVariant.Color))
                .ForMember(dest => dest.SizeType, opt => opt.MapFrom(src => src.SizeType))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductVariant.Product.Price))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ProductVariant.Images
                    .Where(i => i.IsPrimary == true)
                    .Select(i => i.Url)
                    .FirstOrDefault()));

            // Payment 
            CreateMap<AdminPaymentFilterRequest, Repositories.DTOs.AdminPaymentFilterRequest>();

            CreateMap<Payment, AdminPaymentListDto>()
                .ForMember(dest => dest.MethodName, opt => opt.MapFrom(src => src.Method.Name))
                .ForMember(dest => dest.MethodCode, opt => opt.MapFrom(src => src.Method.Code))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Order.Status))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Order.Users.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Order.Users.Name))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Order.Users.Email));

            CreateMap<Payment, AdminPaymentDetailDto>()
                .ForMember(dest => dest.MethodId, opt => opt.MapFrom(src => src.Method.Id))
                .ForMember(dest => dest.MethodName, opt => opt.MapFrom(src => src.Method.Name))
                .ForMember(dest => dest.MethodCode, opt => opt.MapFrom(src => src.Method.Code))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
                .ForMember(dest => dest.OrderTotal, opt => opt.MapFrom(src => src.Order.Total))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Order.Status))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Order.Users.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Order.Users.Name))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Order.Users.Email))
                .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.Order.Users.Phone))
                .ForMember(dest => dest.UserAddress, opt => opt.MapFrom(src => src.Order.Users.Address));


            CreateMap(typeof(PaginationResult<>), typeof(PaginationResult<>));
        }
    }
}
