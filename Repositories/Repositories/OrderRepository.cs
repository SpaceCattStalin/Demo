using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repositories.Basic;
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

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders.Where(o => o.UsersId == userId).Include(o => o.Payment).Include(o => o.Shipping).ToListAsync();
        }

        public async Task<Order> GetOrderAsync (int orderId)
        {
            return await _context.Orders.Where(o => o.Id == orderId).Include(o => o.Payment).Include(o => o.Shipping).Include(o => o.Carts).FirstOrDefaultAsync();
        }
    }
}
