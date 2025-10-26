using Repositories.Basic;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<PaginationResult<ProductThumbnailDto>> GetThumbnailProducts(int categoryId, int currentPage, int pageSize);
    }
}
