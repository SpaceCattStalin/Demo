using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repositories.Basic;
using Repositories.Entities;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(CategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<PaginationResult<CategoryModel>>> GetAll(int page = 1, int pageSize = 10)
        {
            try
            {
                var result = await _categoryService.GetAll(page, pageSize);

                var response = _mapper.Map<PaginationResult<CategoryModel>>(result);
                return response;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }


        }

    }
}
