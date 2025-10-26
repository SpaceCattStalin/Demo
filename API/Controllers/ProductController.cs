using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("by-category")]
        public async Task<IActionResult> GetAllByCategory(int categoryId, int currentPage, int pageSize)
        {
            try
            {
                var products = await _productService.GetThumbnailProducts(categoryId, currentPage, pageSize);

                return Ok(products);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }
    }
}
