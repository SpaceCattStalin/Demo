using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Basic;
using Repositories.Entities;
using Services;
using Services.DTOs;
using Services.Utils;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(OrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        // Xem danh sách đơn hàng của tôi
        // Lấy thông tin tất cả đơn hàng theo user
        [HttpGet("user")]

        public async Task<ActionResult> GetAllUserOrders([FromQuery] UserOrderFilterRequest filter)
        {
            try
            {
                var userPrincipal = HttpContext.User;
                var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                var userId = int.Parse(userIdClaim.Value);

                var result = await _orderService.GetOrderByUser(userId, filter);
                var orders = _mapper.Map<PaginationResult<UserOrderSummaryModel>>(result);

                return Ok(new
                {
                    isSuccess = true,
                    data = orders,
                    pagination = new
                    {
                        result.CurrentPage,
                        result.PageSize,
                        result.TotalItems,
                        result.TotalPages
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.InnerException.Message });
            }
        }

        // Xem chi tiết 1 đơn hàng
        // Lấy thông tin chi tiết 1 đơn hàng theo user
        [HttpGet("user/{orderId}")]
        public async Task<ActionResult> GetOrderById([FromRoute] int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderDetailAsync(orderId);
                if (order == null)
                    return NotFound(new { isSuccess = false, message = "Không tìm thấy đơn hàng." });

                var result = _mapper.Map<UserOrderDetailModel>(order);

                return Ok(new { isSuccess = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.InnerException.Message });
            }
        }

        // Hủy đơn hàng
        [HttpDelete("user/{orderId}")]
        public async Task<ActionResult> CancelOrderById([FromRoute] int orderId, [FromQuery] OrderStatusEnum statusEnum)
        {
            try
            {
                var result = await _orderService.UpdateOrderStatusAsync(orderId, statusEnum);

                if (!result)
                {
                    return BadRequest(new { isSuccess = false, message = "Hủy thất bại" });
                }

                return Ok(new { isSuccess = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.InnerException.Message });
            }
        }

        // Theo dõi đơn hàng (ViettelPost) *làm sau hoặc không làm

        // Thanh toán lại *làm sau hoặc không làm


        //Lấy thông tin đơn hàng theo orderId
        //[Authorize]
        //[HttpGet("{orderId}")]
        //public IActionResult GetDetailOrderByOrderId(int orderId)
        //{
        //    var order = _orderService.GetOrderByIdAsync(orderId).Result;
        //    var orderModel = _mapper.Map<OrderModel>(order);
        //    return Ok(orderModel);
        //}

        //Lấy tất cả đơn hàng của người dùng theo userId
        //[Authorize]
        //[HttpGet("all")]
        //public async Task<IActionResult> GetOrdersByUserId()
        //{
        //    var userPrincipal = HttpContext.User;
        //    var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        //    var userId = int.Parse(userIdClaim.Value);
        //    var orders = await _orderService.GetAllOrdersByUserIdAsync(userId);
        //    var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
        //    return Ok(orderModels);
        //}

        ////Lấy tất cả đơn hàng đang chờ xử lý của người dùng theo userId
        //[Authorize]
        //[HttpGet("pending")]
        //public async Task<IActionResult> GetPendingOrdersByUserId()
        //{
        //    var userPrincipal = HttpContext.User;
        //    var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        //    var userId = int.Parse(userIdClaim.Value);
        //    var orders = await _orderService.GetPendingOrdersByUserIdAsync(userId);
        //    var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
        //    return Ok(orderModels);
        //}

        ////Lấy tất cả đơn hàng đã hoàn thành của người dùng theo userId
        //[Authorize]
        //[HttpGet("completed")]
        //public async Task<IActionResult> GetCompletedOrdersByUserId()
        //{
        //    var userPrincipal = HttpContext.User;
        //    var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        //    var userId = int.Parse(userIdClaim.Value);
        //    var orders = await _orderService.GetCompletedOrdersByUserIdAsync(userId);
        //    var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
        //    return Ok(orderModels);
        //}

        ////Lấy tất cả đơn hàng đã hủy của người dùng theo userId
        //[Authorize]
        //[HttpGet("canceled")]
        //public async Task<IActionResult> GetCancelledOrdersByUserId()
        //{
        //    var userPrincipal = HttpContext.User;
        //    var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        //    var userId = int.Parse(userIdClaim.Value);
        //    var orders = await _orderService.GetCancelledOrdersByUserIdAsync(userId);
        //    var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
        //    return Ok(orderModels);
        //}

        //Tạo đơn hàng từ giỏ hàng của người dùng
        //[Authorize]
        //[HttpPost("create")]
        //public async Task<IActionResult> CreateOrderFromCart(List<int> listCart)
        //{
        //    var userPrincipal = HttpContext.User;
        //    var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        //    var userId = int.Parse(userIdClaim.Value);

        //    var isSuccess = await _orderService.CreateOrderFromCartAsync(userId, listCart);
        //    if (!isSuccess)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok();
        //}

        //Hủy đơn hàng theo orderId
        //[Authorize]
        //[HttpPost("cancel/{orderId}")]
        //public async Task<IActionResult> CancelOrder(int orderId)
        //{
        //    var userPrincipal = HttpContext.User;
        //    var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        //    var userId = int.Parse(userIdClaim.Value);
        //    var isSuccess = await _orderService.CancelOrderAsync(orderId, userId);
        //    if (!isSuccess)
        //    {
        //        return BadRequest(new { message = "Yêu cầu bị từ chối vì đã trong quá trình vận chuyển"});
        //    }
        //    return Ok(new { message = "Yêu cầu huỷ đã được chấp nhận"});
        //}
    }
}
