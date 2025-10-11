using API.DTOs;
using Repositories.Entities;

namespace API.MapperHelper
{
    public class Mapper
    {

        public CartDto MapCartToDto(Cart cart)
        {
            return new CartDto
            {
                CartId = cart.CartId,
                Items = cart.Items.Select(i => new CartItemDto
                {
                    CartItemId = i.CartItemId,
                    Quantity = i.Quantity,
                    Product = new ProductDto
                    {
                        ProductId = i.Product.ProductId,
                        Name = i.Product.Name,
                        Description = i.Product.Description,
                        Price = i.Product.Price,
                        ImageUrl = i.Product.ImageUrl
                    }
                }).ToList()
            };
        }

    }
}
