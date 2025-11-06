using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.DTOs;
using Repositories.DTOs.Services.DTOs;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>
    {
        public PaymentRepository(DemoDbContext context) : base(context)
        {
        }


        public async Task<PaginationResult<Payment>> GetAllPayments(AdminPaymentFilterRequest filter)
        {
            try
            {
                var query = _context.Payments
                    .Include(p => p.Method)
                    .Include(p => p.Order)
                        .ThenInclude(o => o.Users)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(filter.Status))
                    query = query.Where(p => p.Status == filter.Status);

                if (filter.UserId.HasValue)
                    query = query.Where(p => p.Order.UserId == filter.UserId);

                if (filter.MethodId.HasValue)
                    query = query.Where(p => p.Method.Id == filter.MethodId);

                if (filter.FromDate.HasValue)
                    query = query.Where(p => p.CreatedDate >= filter.FromDate);

                if (filter.ToDate.HasValue)
                    query = query.Where(p => p.CreatedDate <= filter.ToDate);

                var totalCount = await query.CountAsync();

                var items = await query
                            .OrderByDescending(p => p.CreatedDate)
                            .Skip((filter.CurrentPage - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ToListAsync();

                return new PaginationResult<Payment>
                {
                    CurrentPage = filter.CurrentPage,
                    PageSize = filter.PageSize,
                    TotalItems = totalCount,
                    TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize),
                    Items = items
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Payment?> GetPaymentById(int id)
        {
            try
            {
                return await _context.Payments
                        .Include(p => p.Method)
                        .Include(p => p.Order)
                            .ThenInclude(o => o.Users)
                        .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PaymentMethod>> GetAllPaymentMethods()
        {
            var methods = await _context.PaymentMethods.ToListAsync();

            return methods;
        }

        public async Task<AdminPaymentStatisticDto> GetPaymentStatisticsAsync(
            int? fromDate = null, int? toDate = null
            )
        {
            var stats = new AdminPaymentStatisticDto();

            var payments = _context.Payments.AsQueryable();

            if (fromDate.HasValue && toDate.HasValue)
            {
                payments = payments.Where(p => p.CreatedDate >= fromDate.Value
                && p.CreatedDate <= toDate.Value);
            }

            stats.TotalPayments = await payments.CountAsync();
            stats.TotalAmountPaid = await payments
                .Where(p => p.Status == "Success")
                .SumAsync(p => p.Amount);

            stats.PendingCount = await payments.CountAsync(p => p.Status == "Pending");
            stats.PaidCount = await payments.CountAsync(p => p.Status == "Success");
            stats.FailedCount = await payments.CountAsync(p => p.Status == "Failed");

            stats.ByMethod = await payments
                .GroupBy(p => new { p.Method.Name, p.Method.Code })
                .Select(g => new PaymentMethodStatistic
                {
                    MethodName = g.Key.Name,
                    MethodCode = g.Key.Code,
                    Count = g.Count(),
                    TotalAmount = g.Where(x => x.Status == "Success").Sum(x => x.Amount)
                })
                .ToListAsync();

            return stats;
        }

    }
}
