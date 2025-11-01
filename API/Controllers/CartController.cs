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

        [HttpGet]
        public async Task<ActionResult> GetCart()
        {
            try
            {
                var userPrincipal = HttpContext.User;
                var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                var userId = int.Parse(userIdClaim.Value);

                var cart = await _cartService.GetAllItemsByUserIdAsync(userId);
                var items = _mapper.Map<List<CartItemDTO>>(cart.Items);

                return Ok(new { isSuccess = true, cartId = cart.Id, items = items, total = items.Sum(item => item.Total) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCart()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);

            var res = await _cartService.CreateCart(userId);

            if (res == 0) return BadRequest(res);

            return Ok(new { success = true, message = "Create cart thành công" });
        }

        //Lấy tất cả giỏ hàng của người dùng theo userId
        //[Authorize]
        //[HttpGet("all")]
        //public async Task<IActionResult> GetCartsByUserId()
        //{
        //    var userPrincipal = HttpContext.User;
        //    var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        //    var userId = int.Parse(userIdClaim.Value);
        //    var carts = await _cartService.GetAllCartsByUserIdAsync(userId);

        //    var cartModels = _mapper.Map<IEnumerable<Cart>, IEnumerable<CartModel>>(carts);

        //    return Ok(cartModels);
        //}

        //Xem thông tin chi tiết sản phẩm trong giỏ hàng theo cartId
        //[Authorize]
        //[HttpGet("{cartId}")]
        //public async Task<IActionResult> GetDetailCartByCartId(int cartId)
        //{
        //    var userPrincipal = HttpContext.User;
        //    var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        //    var userId = int.Parse(userIdClaim.Value);
        //    var cart = await _cartService.GetCartByIdAsync(cartId, userId);
        //    if (cart == null)
        //    {
        //        return NotFound("Không tìm thấy giỏ hàng");
        //    }
        //    var productModel = _mapper.Map<ProductModel>(cart.Product);
        //    return Ok(productModel);
        //}

        //Thêm sản phẩm vào giỏ hàng của người dùng
        [Authorize]
        [HttpPost("add/{productVariantId}")]
        public async Task<IActionResult> AddItemToCart([FromRoute] int productVariantId, int? sizeId)
        {
            try
            {
                var userPrincipal = HttpContext.User;
                var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                var userId = int.Parse(userIdClaim.Value);

                var cart = await _cartService.AddItemToCartAsync(productVariantId, sizeId, userId);

                if (cart == null) return NotFound(new { isSuccess = false, message = $"ProductVariant với id {productVariantId} không tồn tại." });

                var items = _mapper.Map<List<CartItemDTO>>(cart.Items);

                return Ok(new { isSuccess = true, cartId = cart.Id, items = items, total = items.Sum(item => item.Total) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        //Cập nhật số lượng sản phẩm trong giỏ hàng của người dùng
        [Authorize]
        [HttpPut("update")]
        //public async Task<IActionResult> UpdateCartQuantity(int productVariantId, int size, int quantity)
        public async Task<IActionResult> UpdateCartQuantity(int cartItemId, int sizeId, int quantity)
        {
            //var isSuccess = await _cartService.UpdateProductQuantityInCartAsync(cartId, quantity, userId);
            //if (!isSuccess)
            //{
            //    return Forbid("Số lượng sản phẩm vượt quá số lượng có sẵn");
            //}
            try
            {
                var userPrincipal = HttpContext.User;
                var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                var userId = int.Parse(userIdClaim.Value);
                //var updatedCart = await _cartService.UpdateProductQuantityInCartAsync(productVariantId, sizeId, quantity, userId);
                var updatedCart = await _cartService.UpdateProductQuantityInCartAsync(cartItemId, sizeId, quantity, userId);
                var items = _mapper.Map<List<CartItemDTO>>(updatedCart.Items);

                return Ok(new
                {
                    isSuccess = true,
                    cartId = updatedCart.Id,
                    items = items,
                    total = items.Sum(item => item.Total)
                });
                //return Ok("Cập nhật thành công");
            }
            catch (InvalidOperationException ex)
            {
                // lỗi do stock không đủ
                return BadRequest(new
                {
                    isSuccess = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        //Xoá sản phẩm khỏi giỏ hàng của người dùng
        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteItemFromCart(int cartItemId)
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);

            try
            {
                var res = await _cartService.RemoveItemFromCart(cartItemId, userId);

                if (res == 0)
                {
                    return NotFound(new { isSuccess = false, message = "Không tìm thấy Cart item" });
                }

                return Ok(new { isSucess = true, message = "Delete Cart Item thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }

            //var isSuccess = await _cartService.RemoveProductFromCartAsync(cartId, userId);
            //if (!isSuccess)
            //{
            //    return Forbid();
            //}
            //return Ok();
        }

        [Authorize]
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);

            try
            {
                var res = await _cartService.CheckoutAsync(userId, request.PaymentMethodId, request.EndAddress, request.DiscountCode);

                var order = _mapper.Map<OrderModel>(res);

                return Ok(new { isSuccess = true, order = order });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.InnerException.Message });
            }

        }
    }
}
