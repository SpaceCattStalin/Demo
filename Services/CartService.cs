using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Lấy tất cả giỏ hàng của người dùng theo userId
        public async Task<IEnumerable<Cart>> GetAllCartsByUserIdAsync(int userId)
        {
            var allCart = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);
            var cart = allCart.Where(c => c.OrdersId == null);
            return cart;
        }

        //Sản phẩm đã có trong giỏ hàng hay chưa
        public async Task<bool> IsProductInCartAsync(int productId, int userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);
            var isExisted = cart.FirstOrDefault(c => c.ProductId == productId) != null;
            return isExisted;
        }

        //Thêm sản phẩm vào giỏ hàng của người dùng
        public async Task<bool> AddProductToCartAsync(int productId, int userId)
        {
            var numberValid = 0;
            if (await IsProductInCartAsync(productId, userId))
            {
                var cart = (await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId))
                            .FirstOrDefault(c => c.ProductId == productId);
                cart.Quantity += 1;
                cart.Amount += cart.Product.Price;
                numberValid = await _unitOfWork.CartRepository.UpdateAsync(cart);
                if (numberValid == 0)
                {
                    return false;
                }
                return true;
            }
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            var newCart = new Cart
            {
                UsersId = userId,
                ProductId = productId,
                Quantity = 1,
                Amount = product.Price
            };

            numberValid = await _unitOfWork.CartRepository.CreateAsync(newCart);
            if (numberValid == 0)
            {
                return false;
            }
            return true;
        }

        //Cập nhật số lượng sản phẩm trong giỏ hàng
        public async Task<bool> UpdateProductQuantityInCartAsync(int cartId, int quantity, int userId)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(cartId);
            if (cart.UsersId != userId)
            {
                return false;
            }
            if (cart != null)
            {
                cart.Quantity = quantity;
                if (cart.Quantity < 1)
                {
                    cart.Quantity = 1;
                } 
                else if (cart.Quantity > cart.Product.StockQuantity)
                {
                    return false;
                }
                await _unitOfWork.CartRepository.UpdateAsync(cart);
            }
            return true;
        }

        //Xóa sản phẩm khỏi giỏ hàng
        public async Task<bool> RemoveProductFromCartAsync(int cartId, int userId)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(cartId);
            if (cart.UsersId != userId)
            {
                return false;
            }

            await _unitOfWork.CartRepository.RemoveAsync(cart);
            return true;
        }

        //Xóa tất cả sản phẩm khỏi giỏ hàng của người dùng
        public async Task<bool> ClearCartAsync(int userId)
        {
            var carts = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);
            var userCarts = carts.Where(c => c.OrdersId == null).ToList();
            foreach (var cart in userCarts)
            {
                await _unitOfWork.CartRepository.RemoveAsync(cart);
            }
            return true;
        }
    }
}
