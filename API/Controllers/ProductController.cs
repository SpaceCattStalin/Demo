using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Entities;
using Repositories.Repositories;

namespace API.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepo;

        public ProductController(ProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepo.GetAll();

            return Ok(products);
        }
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] Product product)
        //{
        //    await _productRepo.Add(product);
        //    return Ok(product);
        //}

        //// ✏️ PUT: /products/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
        //{
        //    var product = await _productRepo.GetById(id);
        //    if (product == null) return NotFound("Product không tồn tại.");

        //    product.Name = updatedProduct.Name;
        //    product.Description = updatedProduct.Description;
        //    product.Price = updatedProduct.Price;
        //    product.ImageUrl = updatedProduct.ImageUrl;

        //    await _productRepo.Update(product);
        //    return Ok(product);
        //}

        //// ❌ DELETE: /products/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var product = await _productRepo.GetById(id);
        //    if (product == null) return NotFound("Product không tồn tại.");

        //    await _productRepo.Delete(product);
        //    return Ok(new { Message = "Xóa sản phẩm thành công." });
        //}
    }
}
