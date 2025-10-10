using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("cart")]
    [Authorize]
    public class CartController : Controller
    {

        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetMyCart()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(_service.GetUserCart(userId));
        }

        [HttpPost("add")]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = _service.AddToCart(userId, productId, quantity);
            return Ok(result);
        }
        [HttpDelete("remove/{productId}")]
        public IActionResult RemoveFromCart(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var updatedCart = _service.RemoveFromCart(userId, productId);

            if (updatedCart == null)
                return NotFound("Item not found in cart");

            return Ok(updatedCart);
        }
        [HttpPut("update")]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var updatedCart = _service.UpdateCartItem(userId, productId, quantity);

            if (updatedCart == null)
                return NotFound("Product not found in cart");

            return Ok(updatedCart);
        }

    }
}
