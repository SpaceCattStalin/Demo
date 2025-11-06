using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repositories.Basic;
using Services;
using Services.DTOs;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<PaginationResult<ProductModel>>> GetAllWithPaging([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 100)
        {
            if (currentPage <= 0 || pageSize <= 0)
                return BadRequest(new { isSuccess = false, message = "CurrentPage và PageSize phải là số" });

            var items = await _productService.GetAllProductVariants(currentPage, pageSize);

            var result = _mapper.Map<PaginationResult<ProductModel>>(items);

            return Ok(new { isSuccess = true, items = result });
        }

        //[HttpGet("by-category")]
        [HttpGet("search")]
        public async Task<IActionResult> GetAllByCategory([FromQuery] ProductFilterRequest filter)
        {
            try
            {
                var products = await _productService.GetThumbnailProducts(filter);
                var result = _mapper.Map<PaginationResult<ProductModel>>(products);
                //products.Items = (IEnumerable<Repositories.Entities.Product>)_mapper.Map<List<ProductModel>>(products.Items);


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
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductModel>> GetById([FromRoute] int productId)
        {
            try
            {
                var model = await _productService.GetProductWithVariants(productId);
                return _mapper.Map<ProductModel>(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        [HttpGet("{productId}/sizes")]
        public async Task<IActionResult> GetSizesByProductId([FromRoute] int productId)
        {
            try
            {
                var sizes = await _productService.GetSizesByProductIdAsync(productId);
                return Ok(new { isSuccess = true, items = _mapper.Map<List<SizeDTO>>(sizes) });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { isSuccess = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        [HttpGet("images")]
        public async Task<IActionResult> GetImages()
        {
            var images = await _productService.GetIsPrimaryProductImage();

            return Ok(images);
        }
        [HttpGet("{productId}/recommendations")]
        public async Task<IActionResult> GetRecommendations(int productId)
        {
            var result = await _productService.GetRecommendedProductsAsync(productId);
            var mapped = _mapper.Map<List<ProductModel>>(result);

            return Ok(mapped);
        }

        //[HttpPost]
        //public async Task<ActionResult> AddProduct([FromBody] Services.DTOs.CreateProductModel productModel)
        //{
        //    try
        //    {
        //        await _productService.AddProducts(productModel);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
        //    }
        //}

        //[HttpPut]
        //public async Task<ActionResult> UpdateProduct([FromBody] Services.DTOs.UpdateProductModel model)
        //{
        //    try
        //    {
        //        var res = await _productService.UpdateProduct(model);

        //        if (res == 0)
        //        {
        //            return NotFound(new { message = "Update thất bại" });
        //        }

        //        return Ok(new { message = "Update thành công" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
        //    }
        //}
    }
}
