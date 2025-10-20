using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Services;
using Services.Utils;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ShippingService _shippingService;
        private readonly ProductService _productService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        public AdminController(OrderService orderService, ShippingService shippingService, ProductService productService, UserService userService, IMapper mapper)
        {
            _orderService = orderService;
            _shippingService = shippingService;
            _productService = productService;
            _userService = userService;
            _mapper = mapper;
        }

        //Lấy tất cả danh sách user
        [Authorize (Roles = "Admin")]

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var userModels = _mapper.Map<IEnumerable<UserModel>>(users);
            return Ok();
        }

        //Chỉnh sứa trạng thái user (Active/Inactive)
        [Authorize(Roles = "Admin")]
        [HttpPut("users/{userId}/status")]
        public async Task<IActionResult> UpdateUserStatus(int userId, [FromBody] bool isActive)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = isActive;
            var result = await _userService.UpdateUserAsync(user);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi cập nhật trạng thái");
            }
            return NoContent();
        }

        //Quản lý đơn hàng: Lấy tất cả đơn hàng
        [Authorize(Roles = "Admin")]
        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
            return Ok(orderModels);
        }

        //Quản lý vận chuyển: Lấy tất cả thông tin vận chuyển
        [Authorize(Roles = "Admin")]
        [HttpGet("shippings")]
        public async Task<IActionResult> GetAllShippings()
        {
            var shippings = await _shippingService.GetAllShippingsAsync();
            var shippingModels = _mapper.Map<IEnumerable<ShippingModel>>(shippings);
            return Ok(shippingModels);
        }

        //Quản lý sản phẩm: Lấy tất cả sản phẩm
        [Authorize (Roles = "Admin")]
        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var productModels = _mapper.Map<IEnumerable<ProductModel>>(products);
            return Ok(productModels);
        }

        //Quản lý sản phẩm: Thêm sản phẩm mới
        [Authorize(Roles = "Admin")]
        [HttpPost("products")]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel productModel)
        {
            var product = _mapper.Map<Repositories.Entities.Product>(productModel);
            var result = await _productService.AddProductAsync(product);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi thêm dữ liệu");
            }
            return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, productModel);
        }

        //Quản lý sản phẩm: Cập nhật thông tin sản phẩm
        [Authorize(Roles = "Admin")]
        [HttpPut("products/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductModel productModel)
        {
            var existingProduct = await _productService.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _mapper.Map(productModel, existingProduct);
            var result = await _productService.UpdateProductAsync(existingProduct);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Xảy ra lỗi khi cập nhật dữ liệu");
            }
            return NoContent();
        }
        //Quản lý sản phẩm: Xóa sản phẩm
        [Authorize (Roles = "Admin")]
        [HttpDelete("products/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var existingProduct = await _productService.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                return NotFound();
            }
            var result = await _productService.DeleteProductAsync(productId);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Xảy ra lỗi khi xoá dữ liệu");
            }
            return NoContent();
        }

        //Quản lý đơn hàng: Cập nhật trạng thái đơn hàng và vận chuyển thành Shipping
        [Authorize(Roles = "Admin")]
        [HttpPut("orders/{orderId}/status/shipping")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = OrderStatusEnum.Shipping.ToString();

            var isOrderUpdates = await _orderService.UpdateOrderAsync(order);

            var shipping = await _shippingService.GetShippingByIdAsync(orderId);
            if (shipping == null)
            {
                return NotFound();
            }
            var isShippingUpdated = await _shippingService.UpdateShippingStatusAsync(orderId);

            if (!isOrderUpdates || !isShippingUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi cập nhật trạngt thái");
            }
            return NoContent();
        }

    }
}
