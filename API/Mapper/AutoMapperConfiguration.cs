using API.Models;
using AutoMapper;
using Repositories.Entities;

namespace API.Mapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Cart, CartModel>(); 
            CreateMap<Product, ProductModel>();
            CreateMap<Product, CreateProductModel>();
            CreateMap<Product, UpdateProductModel>();
            CreateMap<Order, OrderModel>();
            CreateMap<Payment, PaymentModel>();
            CreateMap<Shipping, ShippingModel>();
            CreateMap<User, UserModel>();
            CreateMap<User, UpdateUserModel>();
        }
    }
}
