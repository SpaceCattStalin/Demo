using Repositories.Basic;
using Repositories.Entities;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        //------------- PRODUCTs ---------------
        Task<PaginationResult<Product>> GetAllProductsByCategory(int categoryId, int currentPage, int pageSize);
        Task<Product?> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int id);

        //------------- Variants ---------------
        Task<ProductVariant> AddVariant(int productId, ProductVariant variant);
        Task<ProductVariant> UpdateVariant(int productId, int variantId, ProductVariant variant);

        //------------- IMAGEs ---------------
        Task<ProductImage> AddProductImage(int productId, ProductImage image);
        Task<ProductImage> AddVariantImage(int productId, int variantId, ProductImage image);
        Task<bool> DeleteImageAsync(int imageId);
    }
}
