using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repositories.Basic;
using Repositories.DTOs;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(DemoDbContext context) : base(context)
        {
        }

        public async Task<PaginationResult<Order>> GetAllOrders(AdminOrderFilterRequest filter)
        {
            var query = _context.Orders
                .Include(o => o.Users)
                .Include(o => o.Payments)
                    .ThenInclude(o => o.Method)
                .Include(o => o.Shippings)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Status))
                query = query.Where(o => o.Status == filter.Status);

            if (!string.IsNullOrEmpty(filter.PaymentStatus))
                query = query.Where(o => o.Payments.Any(p => p.Status == filter.PaymentStatus));

            if (!string.IsNullOrEmpty(filter.ShippingStatus))
                query = query.Where(o => o.Shippings.Any(p => p.Status == filter.ShippingStatus));

            if (filter.FromDate.HasValue)
                query = query.Where(o => o.CreatedDate >= filter.FromDate);

            if (filter.ToDate.HasValue)
                query = query.Where(o => o.CreatedDate <= filter.ToDate);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(o => o.CreatedDate)
                .Take(filter.PageSize)
                .Skip((filter.CurrentPage - 1) * filter.PageSize)
                .ToListAsync();

            return new PaginationResult<Order>
            {
                CurrentPage = filter.CurrentPage,
                PageSize = filter.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize),
                TotalItems = totalCount,
                Items = items
            };

        }
        public async Task<Order?> GetOrderDetailByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Users)
                .Include(o => o.Payments)
                    .ThenInclude(p => p.Method)
                .Include(o => o.Shippings)
                .Include(o => o.Items)
                    .ThenInclude(i => i.ProductVariant)
                        .ThenInclude(v => v.Product)
                .Include(o => o.Items)
                    .ThenInclude(i => i.ProductVariant)
                        .ThenInclude(v => v.Images)
                .Include(o => o.Items)
                        .ThenInclude(ps => ps.Size)
                //.ThenInclude(i => i.ProductVariant)
                //    .ThenInclude(v => v.Sizes)
                //        .ThenInclude(ps => ps.Size)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        //public async Task<PaginationResult<Order>> GetOrdersByUserIdAsync(int userId, int currentPage, int pageSize)
        //{
        //    var query = _context.Orders
        //      .Include(o => o.Users)
        //      .Include(o => o.Payments)
        //          .ThenInclude(p => p.Method)
        //      .Include(o => o.Shippings)
        //      .Include(o => o.Items)
        //          .ThenInclude(i => i.ProductVariant)
        //              .ThenInclude(v => v.Product)
        //      .Include(o => o.Items)
        //          .ThenInclude(i => i.ProductVariant)
        //              .ThenInclude(v => v.Images)
        //      .Include(o => o.Items)
        //              .ThenInclude(ps => ps.Size)
        //      .Where(o => o.UserId == userId)
        //      .AsQueryable();

        //    var totalCount = await query.CountAsync();
        //    var items = await query
        //       .OrderByDescending(o => o.CreatedDate)
        //       .Take(pageSize)
        //       .Skip((currentPage - 1) * pageSize)
        //       .ToListAsync();

        //    return new PaginationResult<Order>
        //    {
        //        CurrentPage = currentPage,
        //        PageSize = pageSize,
        //        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
        //        TotalItems = totalCount,
        //        Items = items
        //    };
        //}
        public async Task<PaginationResult<Order>> GetOrdersByUserIdAsync(int userId, UserOrderFilterRequest filter)
        {
            var query = _context.Orders
                .Include(o => o.Users)
                .Include(o => o.Payments)
                    .ThenInclude(p => p.Method)
                .Include(o => o.Shippings)
                .Include(o => o.Items)
                    .ThenInclude(i => i.ProductVariant)
                        .ThenInclude(v => v.Product)
                .Include(o => o.Items)
                    .ThenInclude(i => i.ProductVariant)
                        .ThenInclude(v => v.Images)
                .Include(o => o.Items)
                    .ThenInclude(ps => ps.Size)
                .Where(o => o.UserId == userId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Status))
                query = query.Where(o => o.Status == filter.Status);

            if (!string.IsNullOrEmpty(filter.PaymentStatus))
                query = query.Where(o => o.Payments.Any(p => p.Status == filter.PaymentStatus));

            if (!string.IsNullOrEmpty(filter.ShippingStatus))
                query = query.Where(o => o.Shippings.Any(s => s.Status == filter.ShippingStatus));

            if (filter.FromDate.HasValue)
                query = query.Where(o => o.CreatedDate >= filter.FromDate);

            if (filter.ToDate.HasValue)
                query = query.Where(o => o.CreatedDate <= filter.ToDate);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(o => o.CreatedDate)
                .Skip((filter.CurrentPage - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PaginationResult<Order>
            {
                CurrentPage = filter.CurrentPage,
                PageSize = filter.PageSize,
                TotalItems = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize),
                Items = items
            };
        }

    }
}
