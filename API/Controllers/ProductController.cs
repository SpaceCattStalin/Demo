using Microsoft.AspNetCore.Mvc;
using Repositories.Basic;
using Services;
using Services.DTOs;
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

        [HttpGet("paging")]
        [ProducesResponseType(typeof(PaginationResult<ProductDto>), 200)]
        public async Task<IActionResult> GetAllPaging(int currentPage, int pageSize)
        {
            try
            {
                var products = await _productService.GetAllProductsPaging(currentPage, pageSize);
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

        [HttpGet("{productId}/variant")]
        [ProducesResponseType(typeof(IEnumerable<ProductVariantDto>), 200)]
        public async Task<IActionResult> GetProductVariants(int productId)
        {
            try
            {
                var variants = await _productService.GetProductVariantsAsync(productId);
                return Ok(variants);
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

        [HttpPost("{productId}/variant")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> AddProductVariants(
            [FromRoute] int productId,
            [FromBody] List<AddProductVariantDto> addProductVariantDtos)
        {
            try
            {
                var result = await _productService.AddProductVariantAsync(productId, addProductVariantDtos);
                return Ok(result);
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

        [HttpPut("{productId}/variant")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateProductVariant(
            [FromRoute] int productId,
            [FromBody] UpdateProductVariantDto updateProductVariantDto)
        {
            try
            {
                var result = await _productService.UpdateProductVariantAsync(productId, updateProductVariantDto);
                return Ok(result);
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

        [HttpDelete("{productId}/variant/{variantId}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteProductVariant(
            [FromRoute] int productId,
            [FromRoute] int variantId)
        {
            try
            {
                var result = await _productService.DeleteProductVariantAsync(productId, variantId);
                return Ok(result);
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
