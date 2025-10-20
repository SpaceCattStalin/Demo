using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        //Lấy tất cả sản phẩm
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            var productModels = _mapper.Map<IEnumerable<Product>>(products);
            return Ok(productModels);
        }

        //Lấy sản phẩm theo productId
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var productModel = _mapper.Map<Product>(product);
            return Ok(productModel);
        }

        //Thêm sản phẩm mới
        [Authorize (Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { productId = product.Id }, product);
        }

        //Cập nhật sản phẩm
        [Authorize (Roles = "Admin")]
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Repositories.Entities.Product product)
        {
            if (productId != product.Id)
            {
                return BadRequest();
            }
            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        //Xóa sản phẩm
        [Authorize (Roles = "Admin")]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productService.DeleteProductAsync(productId);
            return NoContent();
        }
    }
}
