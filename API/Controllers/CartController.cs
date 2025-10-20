using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Services;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        private readonly IMapper _mapper;
        public CartController(CartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        //Lấy tất cả giỏ hàng của người dùng theo userId
        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetCartsByUserId()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var carts = await _cartService.GetAllCartsByUserIdAsync(userId);

            var cartModels = _mapper.Map<IEnumerable<Cart>, IEnumerable<CartModel>>(carts);

            return Ok(cartModels);
        }

        //Thêm sản phẩm vào giỏ hàng của người dùng
        [Authorize]
        [HttpPost("add/{productId}")]
        public async Task<IActionResult> AddProductToCart(int productId)
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            await _cartService.AddProductToCartAsync(productId, userId);
            return Ok();
        }

        //Cập nhật số lượng sản phẩm trong giỏ hàng của người dùng
        [Authorize]
        [HttpPut("update/{cartId}/{quantity}")]
        public async Task<IActionResult> UpdateCartQuantity(int cartId, int quantity)
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var isSuccess = await _cartService.UpdateProductQuantityInCartAsync(cartId, quantity, userId);
            if (!isSuccess)
            {
                return Forbid("Số lượng sản phẩm vượt quá số lượng có sẵn");
            }
            return Ok("Cập nhật thành công");
        }

        //Xoá sản phẩm khỏi giỏ hàng của người dùng
        [Authorize]
        [HttpDelete("delete/{cartId}")]
        public async Task<IActionResult> DeleteProductFromCart(int cartId)
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var isSuccess = await _cartService.RemoveProductFromCartAsync(cartId, userId);
            if (!isSuccess)
            {
                return Forbid();
            }
            return Ok();
        }
    }
}
