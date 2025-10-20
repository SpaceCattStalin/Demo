using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(DemoDbContext context) : base(context)
        {
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }
    }
}
