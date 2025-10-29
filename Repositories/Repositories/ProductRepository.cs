using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.UnitOfWorks;
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

        public async Task<PaginationResult<Product>> GetAllProductsPaging(int currentPage, int pageSize)
        {
            try
            {
                var query = _context.Products
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

        public async Task<int> UpdateProduct(Product product)
        {
            try
            {
                var existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
                if (existingProduct == null)
                {
                    throw new KeyNotFoundException("Product not found");
                }
                _context.Products.Update(existingProduct);
                
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Lỗi khi update Product: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.Variants)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found");
                }

                if (product.Variants != null && product.Variants.Any())
                {
                    _context.Set<ProductVariant>().RemoveRange(product.Variants);
                }

                _context.Products.Remove(product);


                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Lỗi khi delete Product: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<IEnumerable<ProductVariant>> GetVariantsByProductId(int productId)
        {
            try
            {
                var variants = await _context.Set<ProductVariant>()
                    .Where(v => v.ProductId == productId)
                    .Include(v => v.Images)
                    .AsNoTracking()
                    .ToListAsync();
                return variants;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi get all Variant: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<int> UpdateVariant(int productId, ProductVariant variant)
        {
            try
            {
                var existingVariant = await _context.Set<ProductVariant>().FirstOrDefaultAsync(v => v.Id == variant.Id && v.ProductId == productId);
                if (existingVariant == null)
                {
                    throw new KeyNotFoundException("Variant not found");
                }
                if (!string.IsNullOrEmpty(variant.VariantCode))
                {
                    existingVariant.VariantCode = variant.VariantCode;
                }
                if (!string.IsNullOrEmpty(variant.Color))
                {
                    existingVariant.Color = variant.Color;
                }
                if (!string.IsNullOrEmpty(variant.Size))
                {
                    existingVariant.Size = variant.Size;
                }

                _context.Set<ProductVariant>().Update(existingVariant);

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Lỗi khi update Variant: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<bool> DeleteVariant(int productId, int variantId)
        {
            try
            {
                var variant = await _context.Set<ProductVariant>().FirstOrDefaultAsync(v => v.Id == variantId && v.ProductId == productId);
                if (variant == null)
                {
                    throw new KeyNotFoundException("Variant not found");
                }
                _context.Set<ProductVariant>().Remove(variant);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi delete Variant: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<int> AddVariants(int productId, List<ProductVariant> variants)
        {
            try
            {
                foreach (var variant in variants)
                {
                    variant.ProductId = productId;
                }

                _context.Set<ProductVariant>().AddRange(variants);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm mới Variant: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public Task<ProductImage> AddProductImage(int productId, ProductImage image)
        {
            throw new NotImplementedException();
        }

        public Task<ProductImage> AddVariantImage(int productId, int variantId, ProductImage image)
        {
            throw new NotImplementedException();
        }


        public Task<bool> DeleteImageAsync(int imageId)
        {
            throw new NotImplementedException();
        }

    }
}
