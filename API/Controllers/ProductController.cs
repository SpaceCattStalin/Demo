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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]

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

        //[HttpPost]
        //[ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<ProductDto>> Create([FromBody] ProductCreateDto productCreateDto)
        //{
        //    var productToCreate = new Product
        //    {
        //        Name = productCreateDto.Name,
        //        Description = productCreateDto.Description,
        //        Price = productCreateDto.Price,
        //        ImageUrl = "https://curious-pauline-catchable.ngrok-free.dev/Images/placeholder.png",
        //    };
        //    var res = await _productRepo.CreateAsync(productToCreate);

        //    var resultDto = new ProductDto
        //    {
        //        ProductId = productToCreate.ProductId,
        //        Name = productToCreate.Name,
        //        Description = productToCreate.Description,
        //        Price = productToCreate.Price,
        //        ImageUrl = productToCreate.ImageUrl
        //    };

        //    return CreatedAtAction(nameof(GetById), new { id = resultDto.ProductId }, resultDto);
        //}

        //[HttpPut("{id}")]
        //[ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<ProductDto>> Update(int id, [FromBody] ProductCreateDto productDto)
        //{
        //    var product = await _productRepo.GetById(id);
        //    if (product == null)
        //        return NotFound();

        //    // Update fields
        //    product.Name = productDto.Name;
        //    product.Description = productDto.Description;
        //    product.Price = productDto.Price;

        //    await _productRepo.Update(product);

        //    var resultDto = new ProductDto
        //    {
        //        ProductId = product.ProductId,
        //        Name = product.Name,
        //        Description = product.Description,
        //        Price = product.Price,
        //        ImageUrl = product.ImageUrl
        //    };

        //    return Ok(resultDto);
        //}


        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var product = await _productRepo.GetById(id);
        //    if (product == null)
        //        return NotFound();

        //    await _productRepo.Delete(product);
        //    return NoContent();
        //}
    }
}
