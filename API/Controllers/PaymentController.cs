using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly IMapper _mapper;
        public PaymentController(PaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        //Lấy tất cả thông tin thanh toán của người dùng theo userId
        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPaymentsByUserId()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var payments = await _paymentService.GetAllPaymentsByUserIdAsync(userId);
            var paymentModels = _mapper.Map<IEnumerable<PaymentModel>>(payments);
            return Ok(paymentModels);
        }

        //Lấy tất cả thông tin thanh toán thành công của người dùng theo userId
        [Authorize]
        [HttpGet("successful")]
        public async Task<IActionResult> GetSuccessfulPaymentsByUserId()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var payments = await _paymentService.GetSuccessfulPaymentsByUserIdAsync(userId);
            var paymentModels = _mapper.Map<IEnumerable<PaymentModel>>(payments);
            return Ok(paymentModels);
        }

        //Lấy tất cả thông tin thanh toán thất bại của người dùng theo userId
        [Authorize]
        [HttpGet("failed")]
        public async Task<IActionResult> GetFailedPaymentsByUserId()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var payments = await _paymentService.GetFailedPaymentsByUserIdAsync(userId);
            var paymentModels = _mapper.Map<IEnumerable<PaymentModel>>(payments);
            return Ok(paymentModels);
        }

        //Lấy tất cả thông tin thành toán đang chờ xử lý của người dùng theo userId
        [Authorize]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingPaymentsByUserId()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var payments = await _paymentService.GetPendingPaymentsByUserIdAsync(userId);
            var paymentModels = _mapper.Map<IEnumerable<PaymentModel>>(payments);
            return Ok(paymentModels);
        }

        //Ghi nhận thanh toán thành công theo  OrderId
        [Authorize]
        [HttpPost("success/{orderId}")]
        public async Task<IActionResult> PaymentSuccess(int orderId)
        {
            // Giả sử PaymentService có phương thức RecordPaymentSuccessAsync
            var result = await _paymentService.RecordPaymentSuccessAsync(orderId);
            if (result)
            {
                return Ok(new { message = "Đã ghi nhận thanh toán với trạng thái thành công" });
            }
            else
            {
                return BadRequest(new { message = "Xảy ra sự cố khi ghi nhận thanh toán" });
            }
        }

        //Ghi nhận thanh toán thất bại theo OrderId
        [Authorize]
        [HttpPost("failure/{orderId}")]
        public async Task<IActionResult> PaymentFail(int orderId)
        {
            var result = await _paymentService.RecordPaymentFailureAsync(orderId);
            if (result)
            {
                return Ok(new { message = "Đã ghi nhận thanh toán với trạng thái thất bại" });
            }
            else
            {
                return BadRequest(new { message = "Xảy ra sự cố khi ghi nhận thanh toán" });
            }
        }

        
    }
}
