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
    public class ShippingController : ControllerBase
    {
        private readonly ShippingService _shippingService;
        private readonly IMapper _mapper; 
        public ShippingController(ShippingService shippingService, IMapper mapper)
        {
            _shippingService = shippingService;
            _mapper = mapper;
        }

        //Tạo mới thông tin vận chuyển cho đơn hàng
        [Authorize]
        [HttpPost("create/{orderId}")]
        public async Task<IActionResult> CreateShippingForOrder(int orderId, [FromBody] CreateShippingModel shippingModel)
        {
            var shippingEntity = _mapper.Map<Shipping>(shippingModel);
            var result = await _shippingService.CreateShippingForOrderAsync(orderId, shippingEntity);
            if (!result)
            {
                return BadRequest("Tạo thông tin vận chuyển thất bại");
            }
            return Ok("tạo thônng tin vận chuyển thành công");
        }

        //Lấy thông tin chi tiết vận chuyển theo orderId
        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetDetailShippingByShippingId(int orderId)
        {
            var shipping = await _shippingService.GetShippingByIdAsync(orderId);
            if (shipping == null)
            {
                return NotFound("Không tìm thấy thông tin vận chuyển");
            }
            var shippingModel = _mapper.Map<ShippingModel>(shipping);
            return Ok(shippingModel);
        }
    }
}
