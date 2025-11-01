using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Entities;

namespace Repositories.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(DemoDbContext context) : base(context)
        {

        }

        public async Task<PaginationResult<Category>> GetAllWithPaging(int page, int pageSize)
        {
            var query = _context.Categories.Include(p => p.Image);

            var items = await query
                .OrderBy(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItems = await query.CountAsync();
            int totalPages;

            if (totalItems > 0)
            {
                totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            }
            else
            {
                totalPages = 1;
            }
            var result = new PaginationResult<Category>
            (
                items,
                page,
                pageSize,
                totalItems,
                totalPages
            );

            return result;
        }
    }
}
