using Repositories.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CartService
    {
        private readonly ICartRepository _repo;

        public CartService(ICartRepository repo)
        {
            _repo = repo;
        }

        public object? GetUserCart(int userId)
        {
            var cart = _repo.GetCartByUserId(userId);
            if (cart == null) return null;

            return new
            {
                cart.CartId,
                Items = cart.Items.Select(i => new
                {
                    //i.CartItemId,
                    ProductId = i.Product.ProductId,
                    ProductName = i.Product.Name,
                    i.Quantity,
                    //Product = i.Product
                })
            };
        }

        //public CartItem AddToCart(int userId, int productId, int quantity)
        public object? AddToCart(int userId, int productId, int quantity)
        {
            var cart = _repo.GetCartByUserId(userId);

            var item = _repo.GetCartItem(cart.CartId, productId);
            if (item == null)
            {
                item = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _repo.AddCartItem(item);
            }
            else
            {
                item.Quantity += quantity;
                _repo.UpdateCartItem(item);
            }

            _repo.Save();
            var updatedCart = _repo.GetCartByUserId(userId);

            return new
            {
                updatedCart.CartId,
                Items = updatedCart.Items.Select(i => new
                {
                    ProductId = i.Product.ProductId,
                    ProductName = i.Product.Name,
                    i.Quantity,
                })
            };
        }
        public object? RemoveFromCart(int userId, int productId)
        {
            var cart = _repo.GetCartByUserId(userId);
            if (cart == null) return null;

            var item = _repo.GetCartItem(cart.CartId, productId);
            if (item == null) return null;

            _repo.RemoveCartItem(item);
            _repo.Save();

            var updatedCart = _repo.GetCartByUserId(userId);
            return new
            {
                updatedCart.CartId,
                Items = updatedCart.Items.Select(i => new
                {
                    ProductId = i.Product.ProductId,
                    ProductName = i.Product.Name,
                    i.Quantity,
                })
            };
        }
        public object? UpdateCartItem(int userId, int productId, int newQuantity)
        {
            var cart = _repo.GetCartByUserId(userId);
            if (cart == null) return null;

            var item = _repo.GetCartItem(cart.CartId, productId);
            if (item == null) return null;

            if (newQuantity <= 0)
            {
                _repo.RemoveCartItem(item);
            }
            else
            {
                item.Quantity = newQuantity;
                _repo.UpdateCartItem(item);
            }

            _repo.Save();

            var updatedCart = _repo.GetCartByUserId(userId);
            return new
            {
                updatedCart.CartId,
                Items = updatedCart.Items.Select(i => new
                {
                    ProductId = i.Product.ProductId,
                    ProductName = i.Product.Name,
                    i.Quantity,
                })
            };
        }

    }
}
