using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Repositories;
using Services.DTOs;
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

        //public object? GetUserCart(int userId)
        //{
        //    var cart = _repo.GetCartByUserId(userId);
        //    if (cart == null) return null;

        //    return new
        //    {
        //        cart.CartId,
        //        Items = cart.Items.Select(i => new
        //        {
        //            ProductId = i.Product.ProductId,
        //            ProductName = i.Product.Name,
        //            i.Quantity,
        //            ImgUrl = i.Product.ImageUrl
        //        })
        //    };
        //}
        public async Task<Cart> GetUserCart(int userId)
        {
            var cart = await _repo.GetCartByUserId(userId);
            if (cart == null) return null;

            return cart;
        }
        //public CartItem AddToCart(int userId, int productId, int quantity)
        public async Task<Cart> AddToCart(int userId, int productId, int quantity)
        {
            var cart = await _repo.GetCartByUserId(userId);

            var item = await _repo.GetCartItem(cart.CartId, productId);
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
            var updatedCart = await _repo.GetCartByUserId(userId);

            return updatedCart;
        }
        public async Task<Cart> RemoveFromCart(int userId, int productId)
        {
            var cart = await _repo.GetCartByUserId(userId);
            if (cart == null) return null;

            var item = await _repo.GetCartItem(cart.CartId, productId);
            if (item == null) return null;

            _repo.RemoveCartItem(item);
            _repo.Save();

            var updatedCart = await _repo.GetCartByUserId(userId);
            return updatedCart;
        }
        //public async Task<Cart> UpdateCartItem(int userId, int productId, int newQuantity)
        //{
        //    var cart = await _repo.GetCartByUserId(userId);
        //    if (cart == null) return null;

        //    var item = await _repo.GetCartItem(cart.CartId, productId);
        //    if (item == null) return null;

        //    if (newQuantity <= 0)
        //    {
        //        _repo.RemoveCartItem(item);
        //    }
        //    else
        //    {
        //        item.Quantity = newQuantity;
        //        _repo.UpdateCartItem(item);
        //    }

        //    _repo.Save();

        //    var updatedCart = await _repo.GetCartByUserId(userId);
        //    return updatedCart;
        //}

        public async Task<Cart> UpdateCartItem(int userId, int productId, int newQuantity)
        {
            var cart = await _repo.GetCartByUserId(userId);
            if (cart == null) return null;

            var item = await _repo.GetCartItem(cart.CartId, productId);

            // If the item doesn't exist, create it if quantity > 0
            if (item == null)
            {
                if (newQuantity <= 0)
                {
                    // Nothing to do if the requested quantity is 0 or less
                    return cart;
                }

                item = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = newQuantity
                };
                _repo.AddCartItem(item);
            }
            else
            {
                // Existing item: update or remove
                if (newQuantity <= 0)
                    _repo.RemoveCartItem(item);
                else
                    item.Quantity = newQuantity;
            }

            _repo.Save();

            // Return updated cart
            return await _repo.GetCartByUserId(userId);
        }

        //public async Task<Cart?> UpdateCartItems(int userId, List<CartItemRequestDto> items)
        //{
        //    var productQuantities = items.ToDictionary(i => i.ProductId, i => i.Quantity);

        //    return await _repo.UpdateCartItems(userId, productQuantities);
        //}
        public async Task<Cart?> UpdateCartItems(int userId, List<CartItemRequestDto> items)
        {
            var cart = await _repo.GetCartByUserId(userId);
            if (cart == null) return null;

            foreach (var dto in items)
            {
                var item = await _repo.GetCartItem(cart.CartId, dto.ProductId);

                if (item == null)
                {
                    if (dto.Quantity > 0)
                    {
                        item = new CartItem
                        {
                            CartId = cart.CartId,
                            ProductId = dto.ProductId,
                            Quantity = dto.Quantity
                        };
                        _repo.AddCartItem(item);
                    }
                }
                else
                {
                    if (dto.Quantity <= 0)
                        _repo.RemoveCartItem(item);
                    else
                        item.Quantity = dto.Quantity;
                }
            }

            _repo.Save();
            return await _repo.GetCartByUserId(userId);
        }



    }
}
