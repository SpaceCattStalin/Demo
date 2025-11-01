using Microsoft.AspNetCore.Mvc;
using Repositories.Basic;
using Repositories.Entities;
using Repositories.UnitOfWorks;

namespace Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<Category>> GetAll(int currentPage, int pageSize)
        {
            return await _unitOfWork.CategoryRepository.GetAllWithPaging(currentPage, pageSize);
        }
    }
}
