using API.DTOs;
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
        [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]

        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await _productRepo.GetAll();

            var result = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productRepo.GetById(id);
            var result = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };

            return Ok(result);
        }
    }
}
