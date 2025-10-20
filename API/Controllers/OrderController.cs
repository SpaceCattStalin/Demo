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
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(OrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        //Lấy tất cả đơn hàng của người dùng theo userId
        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetOrdersByUserId()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var orders = await _orderService.GetAllOrdersByUserIdAsync(userId);
            var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
            return Ok(orderModels);
        }

        //Lấy tất cả đơn hàng đang chờ xử lý của người dùng theo userId
        [Authorize]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingOrdersByUserId()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var orders = await _orderService.GetPendingOrdersByUserIdAsync(userId);
            var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
            return Ok(orderModels);
        }

        //Lấy tất cả đơn hàng đã hoàn thành của người dùng theo userId
        [Authorize]
        [HttpGet("completed")]
        public async Task<IActionResult> GetCompletedOrdersByUserId()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var orders = await _orderService.GetCompletedOrdersByUserIdAsync(userId);
            var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
            return Ok(orderModels);
        }

        //Lấy tất cả đơn hàng đã hủy của người dùng theo userId
        [Authorize]
        [HttpGet("canceled")]
        public async Task<IActionResult> GetCancelledOrdersByUserId()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var orders = await _orderService.GetCancelledOrdersByUserIdAsync(userId);
            var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
            return Ok(orderModels);
        }

        //Tạo đơn hàng từ giỏ hàng của người dùng
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderFromCart(List<int> listCart)
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);

            var isSuccess = await _orderService.CreateOrderFromCartAsync(userId, listCart);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }

        //Hủy đơn hàng theo orderId
        [Authorize]
        [HttpPost("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var isSuccess = await _orderService.CancelOrderAsync(orderId, userId);
            if (!isSuccess)
            {
                return BadRequest(new { message = "Yêu cầu bị từ chối vì đã trong quá trình vận chuyển"});
            }
            return Ok(new { message = "Yêu cầu huỷ đã được chấp nhận"});
        }
    }
}
