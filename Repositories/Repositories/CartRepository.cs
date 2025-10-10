using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface ICartRepository
    {
        Cart? GetCartByUserId(int userId);
        CartItem? GetCartItem(int cartId, int productId);
        void AddCartItem(CartItem item);
        void UpdateCartItem(CartItem item);
        void RemoveCartItem(CartItem item);

        void Save();
    }

    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cart? GetCartByUserId(int userId)
        {
            return _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.UserId == userId);
        }

        public CartItem? GetCartItem(int cartId, int productId)
        {
            return _context.CartItems
                .FirstOrDefault(i => i.CartId == cartId && i.ProductId == productId);
        }

        public void AddCartItem(CartItem item)
        {
            _context.CartItems.Add(item);
        }

        public void UpdateCartItem(CartItem item)
        {
            _context.CartItems.Update(item);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void RemoveCartItem(CartItem item)
        {
            _context.CartItems.Remove(item);
        }
    }
}
