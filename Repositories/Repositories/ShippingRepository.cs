using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.DTOs;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ShippingRepository : GenericRepository<Shipping>
    {
        public ShippingRepository(DemoDbContext context) : base(context)
        {
        }
        public async Task<PaginationResult<Shipping>> GetAllWithFilter(AdminOrderFilterRequest filter)
        {
            var query = _context.Shippings
                .Include(s => s.Order)
                    .ThenInclude(o => o.Users)
                .Include(s => s.Order)
                    .ThenInclude(o => o.Items)
                        .ThenInclude(i => i.ProductVariant)
                            .ThenInclude(p => p.Sizes)
                                .ThenInclude(sz => sz.Size)
                .Include(s => s.Order)
                    .ThenInclude(o => o.Items)
                        .ThenInclude(i => i.ProductVariant)
                            .ThenInclude(p => p.Product)
                .AsQueryable();

            // --- Filtering ---
            //if (!string.IsNullOrEmpty(filter.Keyword))
            //{
            //    query = query.Where(s =>
            //        s.Order.Users.Name.Contains(filter.Keyword) ||
            //        s.Order.Users.Email.Contains(filter.Keyword) ||
            //        s.Order.Id.ToString().Contains(filter.Keyword));
            //}

            if (!string.IsNullOrEmpty(filter.ShippingStatus))
                query = query.Where(s => s.Status == filter.ShippingStatus);

            if (filter.UserId.HasValue)
                query = query.Where(s => s.Order.UserId == filter.UserId.Value);

            if (filter.FromDate.HasValue)
                query = query.Where(s => s.StartDate >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(s => s.StartDate <= filter.ToDate.Value);

            // --- Sorting ---
            //query = filter.IsDescending
            //    ? query.OrderByDescending(GetSortExpression(filter.SortBy))
            //    : query.OrderBy(GetSortExpression(filter.SortBy));

            // --- Pagination ---
            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(s => s.CreatedDate)
                .Skip((filter.CurrentPage - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            int totalPages = totalItems > 0
                ? (int)Math.Ceiling(totalItems / (double)filter.PageSize)
                : 1;

            return new PaginationResult<Shipping>(items, filter.CurrentPage, filter.PageSize, totalItems, totalPages);
        }

        // Helper: dynamic sorting
        //private static Expression<Func<Shipping, object>> GetSortExpression(string? sortBy)
        //{
        //    return sortBy?.ToLower() switch
        //    {
        //        "status" => s => s.Status,
        //        "startdate" => s => s.StartDate,
        //        "finishdate" => s => s.FinishDate,
        //        _ => s => s.Id
        //    };
        //}

        public async Task<Shipping?> GetDetailAsync(int id)
        {
            return await _context.Shippings
                .Include(s => s.Order)
                    .ThenInclude(o => o.Users)
                .Include(s => s.Order)
                    .ThenInclude(o => o.Items)
                        .ThenInclude(i => i.ProductVariant)
                            .ThenInclude(v => v.Product)
                .Include(s => s.Order)
                    .ThenInclude(o => o.Items)
                        .ThenInclude(i => i.ProductVariant)
                            .ThenInclude(p => p.Sizes)
                                .ThenInclude(sz => sz.Size)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
