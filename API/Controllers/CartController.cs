using API.DTOs;
using API.MapperHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTOs;
using System;
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

        // A private property to get the user ID, avoiding code repetition.
        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public CartController(CartService service)
        {
            _service = service;
            _mapper = new Mapper();
        }

        [HttpGet]
        [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CartDto>> GetUserCart()
        {
            try
            {
                var cart = await _service.GetUserCart(UserId);

                if (cart == null)
                {
                    return NotFound("Cart not found");
                }

                // Use the mapper for consistency
                var cartDto = _mapper.MapCartToDto(cart);
                return Ok(cartDto);
            }
            catch (Exception ex)
            {
                // In a real application, you should log the exception details (ex.ToString())
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while retrieving the cart.");
            }
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CartDto>> AddToCart([FromBody] CartItemRequestDto item)
        {
            try
            {
                var cart = await _service.AddToCart(UserId, item.ProductId, item.Quantity);
                var cartDto = _mapper.MapCartToDto(cart);
                return Ok(cartDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while adding the item.");
            }
        }

        [HttpDelete("remove/{productId}")]
        [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CartDto>> RemoveFromCart(int productId)
        {
            try
            {
                var updatedCart = await _service.RemoveFromCart(UserId, productId);

                if (updatedCart == null)
                {
                    return NotFound("Item not found in cart");
                }

                var cartDto = _mapper.MapCartToDto(updatedCart);
                return Ok(cartDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while removing the item.");
            }
        }

        [HttpPut("update-items")]
        public async Task<ActionResult<CartDto>> UpdateCartItems([FromBody] List<CartItemRequestDto> items)
        {
            try
            {
                var updatedCart = await _service.UpdateCartItems(UserId, items);

                if (updatedCart == null)
                    return NotFound("Cart not found");

                var cartDto = _mapper.MapCartToDto(updatedCart);
                return Ok(cartDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while updating the quantity.");
            }
        }

    }

    //public class CartItemRequestDto
    //{
    //    public int ProductId { get; set; }
    //    public int Quantity { get; set; }
    //}
}