using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Repositories.Basic;
using Repositories.Entities;
using Services;
using Services.DTOs;
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
        private readonly PaymentService _paymentService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        public AdminController(OrderService orderService, ShippingService shippingService,
            ProductService productService, UserService userService,
            PaymentService paymentService, IMapper mapper)
        {
            _orderService = orderService;
            _shippingService = shippingService;
            _productService = productService;
            _userService = userService;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        //Lấy tất cả danh sách user
        [Authorize(Roles = "Admin")]

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                var userModels = _mapper.Map<IEnumerable<UserModel>>(users);
                return Ok(new { isSuccess = true, data = userModels });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });

            }
        }

        //Chỉnh sứa trạng thái user (Active/Inactive)
        //[Authorize(Roles = "Admin")]
        //[HttpPut("users/{userId}/status")]
        //public async Task<IActionResult> UpdateUserStatus(int userId, [FromBody] bool isActive)
        //{
        //    var user = await _userService.GetUserByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    user.IsActive = isActive;
        //    var result = await _userService.UpdateUserAsync(user);
        //    if (!result)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi cập nhật trạng thái");
        //    }
        //    return NoContent();
        //}


        [HttpPost("product")]
        public async Task<ActionResult> AddProduct([FromBody] Services.DTOs.CreateProductModel productModel)
        {
            try
            {
                await _productService.AddProducts(productModel);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        [HttpPut("product")]
        public async Task<ActionResult> UpdateProduct([FromBody] Services.DTOs.UpdateProductModel model)
        {
            try
            {
                var res = await _productService.UpdateProduct(model);

                if (res == 0)
                {
                    return NotFound(new { message = "Update thất bại" });
                }

                return Ok(new { message = "Update thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        [HttpDelete("product/{productId}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int productId)
        {
            try
            {
                var res = await _productService.DeleteProduct(productId);

                if (res == 0)
                {
                    return NotFound(new { message = "Delete thất bại" });
                }

                return Ok(new { message = "Delete thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }
        //Lấy thông tin tất cả đơn hàng cho Admin
        //Quản lý đơn hàng: Lấy tất cả đơn hàng
        [Authorize(Roles = "Admin")]
        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] Services.DTOs.AdminOrderFilterRequest filter)
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync(filter);

                var result = _mapper.Map<PaginationResult<AdminOrderModel>>(orders);

                return Ok(new { isSuccess = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("orders/{orderId}")]
        public async Task<IActionResult> GetOrderDetail([FromRoute] int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderDetailAsync(orderId);
                if (order == null)
                    return NotFound(new { isSuccess = false, message = "Không tìm thấy đơn hàng." });

                var result = _mapper.Map<AdminOrderDetailModel>(order);

                return Ok(new { isSuccess = true, data = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }
        [HttpGet("shippings")]
        public async Task<IActionResult> GetAll([FromQuery] AdminOrderFilterRequest filter)
        {
            try
            {
                var result = await _shippingService.GetAllShippingsAsync(filter);
                var mappedItems = _mapper.Map<List<ShippingDetailDto>>(result.Items);

                return Ok(new
                {
                    isSuccess = true,
                    data = mappedItems,
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
                return StatusCode(500, new { isSuccess = false, message = ex.Message });
            }
        }

        // GET /api/shippings/{id}
        [HttpGet("shippings/{shippingId}")]
        public async Task<IActionResult> GetDetail(int shippingId)
        {
            try
            {
                var shipping = await _shippingService.GetShippingDetailAsync(shippingId);
                var mappedShipping = _mapper.Map<ShippingDetailDto>(shipping);

                if (shipping == null)
                    return NotFound(new { isSuccess = false, message = "Không tìm thấy đơn vận chuyển" });

                return Ok(new { isSuccess = true, data = mappedShipping });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { isSuccess = false, message = ex.Message });
            }
        }

        //Lấy thông tin hết các payments của mọi người dùng
        [HttpGet("payments")]
        public async Task<IActionResult> GetAllPayments([FromQuery] Services.DTOs.AdminPaymentFilterRequest filter)
        {
            try
            {
                var mappedFilter = _mapper.Map<Repositories.DTOs.AdminPaymentFilterRequest>(filter);
                var result = await _paymentService.GetAllPayments(mappedFilter);

                var mappedItems = _mapper.Map<List<AdminPaymentListDto>>(result.Items);

                return Ok(new
                {
                    isSuccess = true,
                    data = mappedItems,
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
                return StatusCode(500, new { isSuccess = false, message = ex.Message });
            }
        }

        //Lấy detail của một payment
        [HttpGet("payments/{paymentId}")]
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            try
            {
                var result = await _paymentService.GetPaymentById(paymentId);
                var mappedPayment = _mapper.Map<AdminPaymentDetailDto>(result);

                if (result == null)
                    return NotFound(new { isSuccess = false, message = "Không tìm thấy payment" });

                return Ok(new { isSuccess = true, data = mappedPayment });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPut("/payments/{paymentId}/status")]
        public async Task<IActionResult> UpdatePaymentStatus(int paymentId, [FromQuery] PaymentStatusEnum paymentStatusEnum)
        {
            try
            {
                var result = await _paymentService.UpdatePaymentStatus(paymentId, paymentStatusEnum);

                if (!result)
                {
                    return BadRequest(new { isSuccess = false, message = "Cập nhật thất bại" });
                }

                return Ok(new { isSuccess = true, message = "Cập nhật thành công" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { isSuccess = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("payments/statistics")]
        public async Task<IActionResult> GetPaymentStatistics([FromQuery] int? fromDate, [FromQuery] int? toDate)
        {
            try
            {
                var stats = await _paymentService.GetPaymentStatisticsAsync(fromDate, toDate);
                return Ok(new { isSuccess = true, statistics = stats });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        //[Authorize(Roles = "Admin")]
        //[HttpGet("orders/{orderId}")]
        //public async Task<IActionResult> GetOrderDetail(int orderId)
        //{
        //    try
        //    {
        //        var order = await _orderService.GetOrderByIdAsync(orderId);
        //        if (order == null)
        //            return NotFound(new { isSuccess = false, message = "Không tìm thấy đơn hàng." });

        //        var mapped = _mapper.Map<AdminOrderDetailModel>(order);
        //        return Ok(new { isSuccess = true, order = mapped });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
        //    }
        //}

        //Quản lý vận chuyển: Lấy tất cả thông tin vận chuyển
        //[Authorize(Roles = "Admin")]
        //[HttpGet("shippings")]
        //public async Task<IActionResult> GetAllShippings()
        //{
        //    var shippings = await _shippingService.GetAllShippingsAsync();
        //    var shippingModels = _mapper.Map<IEnumerable<ShippingModel>>(shippings);
        //    return Ok(shippingModels);
        //}

        //Quản lý sản phẩm: Lấy tất cả sản phẩm
        //[Authorize(Roles = "Admin")]
        //[HttpGet("products")]
        //public async Task<IActionResult> GetAllProducts()
        //{
        //    var products = await _productService.GetAllProductsAsync();
        //    var productModels = _mapper.Map<IEnumerable<ProductModel>>(products);
        //    return Ok(productModels);
        //}

        //Quản lý sản phẩm: Thêm sản phẩm mới
        //[Authorize(Roles = "Admin")]
        //[HttpPost("products")]
        //public async Task<IActionResult> AddProduct([FromBody] ProductModel productModel)
        //{
        //    var product = _mapper.Map<Repositories.Entities.Product>(productModel);
        //    var result = await _productService.AddProductAsync(product);
        //    if (!result)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi thêm dữ liệu");
        //    }
        //    return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, productModel);
        //}

        //Quản lý sản phẩm: Cập nhật thông tin sản phẩm
        //[Authorize(Roles = "Admin")]
        //[HttpPut("products/{productId}")]
        //public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductModel productModel)
        //{
        //    var existingProduct = await _productService.GetProductByIdAsync(productId);
        //    if (existingProduct == null)
        //    {
        //        return NotFound();
        //    }
        //    _mapper.Map(productModel, existingProduct);
        //    var result = await _productService.UpdateProductAsync(existingProduct);
        //    if (!result)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Xảy ra lỗi khi cập nhật dữ liệu");
        //    }
        //    return NoContent();
        //}
        //Quản lý sản phẩm: Xóa sản phẩm
        //[Authorize(Roles = "Admin")]
        //[HttpDelete("products/{productId}")]
        //public async Task<IActionResult> DeleteProduct(int productId)
        //{
        //    var existingProduct = await _productService.GetProductByIdAsync(productId);
        //    if (existingProduct == null)
        //    {
        //        return NotFound();
        //    }
        //    var result = await _productService.DeleteProductAsync(productId);
        //    if (!result)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Xảy ra lỗi khi xoá dữ liệu");
        //    }
        //    return NoContent();
        //}

        //Quản lý đơn hàng: Cập nhật trạng thái đơn hàng
        //[Authorize(Roles = "Admin")]
        //[HttpPut("orders/{orderId}/status")]
        //public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string status)
        //{
        //    var order = await _orderService.GetOrderByIdAsync(orderId);
        //    if (order == null)
        //        return NotFound();

        //    if (!Enum.TryParse<OrderStatusEnum>(status, out var newStatus))
        //        return BadRequest(new { message = "Trạng thái không hợp lệ." });

        //    order.Status = newStatus.ToString();
        //    var result = await _orderService.UpdateOrderAsync(order);

        //    if (!result)
        //        return StatusCode(500, new { message = "Lỗi khi cập nhật trạng thái." });

        //    return NoContent();
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpDelete("orders/{orderId}/cancel")]
        //public async Task<IActionResult> CancelOrder(int orderId)
        //{
        //    var order = await _orderService.GetOrderByIdAsync(orderId);
        //    if (order == null)
        //        return NotFound();

        //    if (order.Status == OrderStatusEnum.Delivered.ToString())
        //        return BadRequest(new { message = "Không thể hủy đơn đã giao." });

        //    order.Status = OrderStatusEnum.Cancelled.ToString();
        //    await _orderService.UpdateOrderAsync(order);
        //    return Ok(new { isSuccess = true, message = "Đơn hàng đã bị hủy." });
        //}


        //Quản lý đơn hàng: Cập nhật trạng thái đơn hàng và vận chuyển thành Shipping
        //[Authorize(Roles = "Admin")]
        //[HttpPut("orders/{orderId}/status/shipping")]
        //public async Task<IActionResult> UpdateOrderStatus(int orderId)
        //{
        //    var order = await _orderService.GetOrderByIdAsync(orderId);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    order.Status = OrderStatusEnum.Shipping.ToString();

        //    var isOrderUpdates = await _orderService.UpdateOrderAsync(order);

        //    var shipping = await _shippingService.GetShippingByIdAsync(orderId);
        //    if (shipping == null)
        //    {
        //        return NotFound();
        //    }
        //    var isShippingUpdated = await _shippingService.UpdateShippingStatusAsync(orderId);

        //    if (!isOrderUpdates || !isShippingUpdated)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi cập nhật trạng thái");
        //    }
        //    return NoContent();
        //}

    }
}
