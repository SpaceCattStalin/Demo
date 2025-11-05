using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.DTOs;
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


        //public async Task<PaginationResult<Product>> GetAllProductsByCategory(int categoryId,
        //    int currentPage, int pageSize)
        //{
        //    try
        //    {
        //        var query = _context.Products
        //          .Where(p => p.CategoryId == categoryId)
        //          .Include(p => p.Images.Where(i => i.IsPrimary));

        //        var totalItems = await query.CountAsync();

        //        var items = await query
        //           .Skip((currentPage - 1) * pageSize)
        //           .Take(pageSize)
        //           .ToListAsync();

        //        int totalPages;

        //        if (totalItems > 0)
        //        {
        //            totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        //        }
        //        else
        //        {
        //            totalPages = 1;
        //        }

        //        return new PaginationResult<Product>(items, currentPage, pageSize, totalItems, totalPages);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Lỗi khi get all Product: {ex.InnerException?.Message ?? ex.Message}");
        //    }
        //}

        public async Task<List<string>> GetPrimaryProductImages()
        {
            try
            {
                //var products = await _context.Products
                //    .Include(p => p.Images)
                //    .GroupBy(p => p.CategoryId)
                //    .Select(g => g.First())
                //    .ToListAsync();


                //var urls = products
                //    .Select(p => p.Images
                //        .FirstOrDefault(img => img.IsPrimary)?.Url
                //    )
                //    .ToList();
                var categories = await _context.Categories
                   .Include(c => c.Image)
                   .ToListAsync();

                var urls = categories
                    .Where(c => c.Image != null)
                    .Select(c => c.Image.Url)
                    .ToList();

                return urls;

                return urls;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<PaginationResult<Product>> GetAllProductsByCategory(
          ProductFilterRequest filter)
        {
            try
            {
                var query = _context.Products
                     .Include(p => p.Images)
                                    .Include(p => p.Variants)
                                        .ThenInclude(ps => ps.Sizes)
                                    .Include(p => p.Variants)
                                        .ThenInclude(v => v.Images)
                                    .Include(p => p.Variants)
                                        .ThenInclude(v => v.Sizes)
                                            .ThenInclude(sz => sz.Size)
                           .Include(p => p.Images.Where(i => i.IsPrimary))
                           .AsQueryable();

                if (filter.CategoryId != 0)
                {
                    query = query.Where(p => p.CategoryId == filter.CategoryId);
                }

                //var query = _context.Products
                //  .Where(p => p.CategoryId == filter.CategoryId)
                //  .Include(p => p.Images.Where(i => i.IsPrimary))
                //  .AsQueryable();

                // Filter by Keyword (Name or Description)
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    query = query.Where(p => p.Name.Contains(filter.Keyword));
                }
                if (filter.Colors != null && filter.Colors.Any())
                {
                    query = query.Where(p => p.Variants
                        .Any(v => filter.Colors.Contains(v.Color)));
                }

                // Filter by multiple Sizes
                if (filter.Sizes != null && filter.Sizes.Any())
                {
                    query = query.Where(p => p.Variants
                        .Any(v => v.Sizes
                            .Any(s => filter.Sizes.Contains(s.Size.SizeType))));
                }

                // Filter by Min Price
                if (filter.MinPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= filter.MinPrice.Value);
                }

                // Filter by Max Price
                if (filter.MaxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= filter.MaxPrice.Value);
                }

                // Sorting
                query = filter.SortBy?.ToLower() switch
                {
                    "price" => filter.IsDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    "name" => filter.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    "createdate" => filter.IsDescending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt),
                    _ => query.OrderByDescending(p => p.CreatedAt) // Default sort
                };

                var totalItems = await query.CountAsync();

                var items = await query
                   .Skip((filter.CurrentPage - 1) * filter.PageSize)
                   .Take(filter.PageSize)
                   .ToListAsync();

                int totalPages;

                if (totalItems > 0)
                {
                    totalPages = (int)Math.Ceiling(totalItems / (double)filter.PageSize);
                }
                else
                {
                    totalPages = 1;
                }

                return new PaginationResult<Product>
                {
                    CurrentPage = filter.CurrentPage,
                    PageSize = filter.PageSize,
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling((double)totalItems / filter.PageSize),
                    Items = items
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi get all Product: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<int> DeleteProduct(int id)
        {
            try
            {
                var productToDelete = await _context.Products.FindAsync(id);

                if (productToDelete != null)
                {
                    productToDelete.IsAvailable = false;
                }

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi delete Product: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
        public async Task<Product?> GetProductWithVariantById(int id)
        {
            try
            {
                var product = await _context.Products
                     .Include(p => p.Images)
                     .Include(p => p.Variants)
                         .ThenInclude(v => v.Sizes)
                            .ThenInclude(ps => ps.Size)
                     .Include(p => p.Variants)
                         .ThenInclude(v => v.Images)
                     .AsNoTracking()
                     .FirstOrDefaultAsync(p => p.Id == id);

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi get by id Product: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
        public async Task<ProductVariant?> GetVariantWithSizesAsync(int variantId)
        {
            return await _context.ProductVariants
                .Include(v => v.Sizes)
                    .ThenInclude(ps => ps.Size)
                .FirstOrDefaultAsync(v => v.Id == variantId);
        }
        public async Task<PaginationResult<Product>> GetAllProductVariants(int currentPage, int pageSize)
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.Images)
                    .Include(p => p.Variants)
                        .ThenInclude(ps => ps.Sizes)
                    .Include(p => p.Variants)
                        .ThenInclude(v => v.Images)
                    .Include(p => p.Variants)
                        .ThenInclude(v => v.Sizes)
                            .ThenInclude(sz => sz.Size)
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginationResult<Product>
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    Items = products,
                    TotalItems = products.Count,
                    TotalPages = (int)Math.Ceiling((double)products.Count / pageSize),
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi get all Product Variant: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
        //public async Task<Product> GetProductByIdAsync(int productId)
        //{
        //    return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        //}

        public async Task<int> UpdateProductWithImage(UpdateProductDAO productUpdateDAO)
        {
            try
            {
                var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == productUpdateDAO.Id);
                if (product != null)
                {
                    product.Name = productUpdateDAO.Name;
                    product.Price = productUpdateDAO.Price;

                }

                foreach (var imgModel in productUpdateDAO.Images)
                {
                    var existingImg = product.Images.FirstOrDefault(i => i.Id == imgModel.Id);

                    if (existingImg != null)
                    {
                        existingImg.Url = imgModel.Url;
                    }
                }

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi update Product: {ex.InnerException?.Message ?? ex.Message}");
            }
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
    }
}
