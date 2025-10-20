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
    public class CartRepository : GenericRepository<Cart>
    {
        public CartRepository(DemoDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cart>> GetCartsByUserIdAsync(int userId)
        {
            return await _context.Carts.Where(c => c.UsersId == userId).Include(p => p.Product).ToListAsync();
        }

    }
}
