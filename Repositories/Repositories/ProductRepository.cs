using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    //public class ProductRepository : GenericRepository<Product>, IProductRepository
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(DemoDbContext context) : base(context)
        {
        }


        public async Task<PaginationResult<Product>> GetAllProductsByCategory(int categoryId,
            int currentPage, int pageSize)
        {
            try
            {
                var query = _context.Products
                  .Where(p => p.CategoryId == categoryId)
                  .Include(p => p.Images.Where(i => i.IsPrimary));

                var totalItems = await query.CountAsync();

                var items = await query
                   .Skip((currentPage - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync();

                return new PaginationResult<Product>(items, currentPage, pageSize, totalItems);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi get all Product: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            try
            {
                var product = await _context.Products
                     .Include(p => p.Images)
                     .Include(p => p.Variants)
                         .ThenInclude(v => v.Images)
                     .AsNoTracking()
                     .FirstOrDefaultAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi get by id Product: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        //public async Task<Product> GetProductByIdAsync(int productId)
        //{
        //    return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        //}

        public Task<Product> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductVariant> UpdateVariant(int productId, int variantId, ProductVariant variant)
        {
            throw new NotImplementedException();
        }
        public Task<ProductImage> AddProductImage(int productId, ProductImage image)
        {
            throw new NotImplementedException();
        }

        public Task<ProductVariant> AddVariant(int productId, ProductVariant variant)
        {
            throw new NotImplementedException();
        }

        public Task<ProductImage> AddVariantImage(int productId, int variantId, ProductImage image)
        {
            throw new NotImplementedException();
        }

        public Task<Product> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteImageAsync(int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

    }
}
