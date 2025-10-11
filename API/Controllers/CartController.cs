using API.DTOs;
using API.MapperHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("cart")]
    [Authorize]
    public class CartController : Controller
    {

        private readonly CartService _service;
        private readonly Mapper _mapper;
        public CartController(CartService service)
        {
            _service = service;
            _mapper = new Mapper();
        }

        [HttpGet]
        [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CartDto>> GetUserCart()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cart = await _service.GetUserCart(userId);

            if (cart == null)
                return NotFound("Cart not found");

            var cartDto = new CartDto
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

            return Ok(cartDto);
        }

        [HttpPost("add")]
        public async Task<ActionResult<CartDto>> AddToCart(int productId, int quantity)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cart = await _service.AddToCart(userId, productId, quantity);

            var cartDto = _mapper.MapCartToDto(cart);
            return Ok(cartDto);
        }

        [HttpDelete("remove/{productId}")]
        public async Task<ActionResult<CartDto>> RemoveFromCart(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var updatedCart = await _service.RemoveFromCart(userId, productId);

            if (updatedCart == null)
                return NotFound("Item not found in cart");

            var cartDto = _mapper.MapCartToDto(updatedCart);
            return Ok(cartDto);
        }

        [HttpPut("update")]
        public async Task<ActionResult<CartDto>> UpdateQuantity(int productId, int quantity)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var updatedCart = await _service.UpdateCartItem(userId, productId, quantity);

            if (updatedCart == null)
                return NotFound("Product not found in cart");

            var cartDto = _mapper.MapCartToDto(updatedCart);
            return Ok(cartDto);
        }


    }
}
