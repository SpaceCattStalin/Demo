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

        public async Task<int> CreateCart(int userId)
        {
            var cart = new Cart
            {
                UsersId = userId,
            };

            await _context.Carts.AddAsync(cart);

            return await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCart(int userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(cart => cart.UsersId == userId);
        }
        public async Task<Cart> GetCartsByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                    .ThenInclude(i => i.ProductVariant)
                        .ThenInclude(v => v.Product)
                .Include(c => c.Items)
                    .ThenInclude(i => i.ProductVariant)
                        .ThenInclude(v => v.Sizes)
                            .ThenInclude(ps => ps.Size)
                .Include(c => c.Items)
                    .ThenInclude(i => i.ProductVariant)
                        .ThenInclude(v => v.Images)
                .Include(c => c.Items)
                    .ThenInclude(i => i.Size)
                    .FirstOrDefaultAsync(c => c.UsersId == userId);
        }
    }
}
