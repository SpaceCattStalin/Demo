using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.UnitOfWorks;
using Services.Utils;
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

        public async Task<int> CreateCart(int userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCart(userId);

            if (cart == null)
            {
                throw new Exception("Cart đã tồn tại");
            }

            return await _unitOfWork.CartRepository.CreateCart(userId);
        }

        //Lấy tất cả  item trong giỏ hàng của người dùng theo userId
        public async Task<Cart> GetAllItemsByUserIdAsync(int userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);

            return cart;
        }

        //Lấy thông tin chi tiết sản phẩm trong giỏ hàng theo cartId
        public async Task<Cart?> GetCartByIdAsync(int cartId, int userId)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(cartId);
            if (cart == null || cart.UsersId != userId)
            {
                return null;
            }
            return cart;
        }

        //Sản phẩm đã có trong giỏ hàng hay chưa
        //public async Task<bool> IsProductInCartAsync(int productVariantId, int userId)
        //{
        //    var cart = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);

        //    if(cart == null || cart.UsersId != userId) { return false; }

        //    return cart.Items.Any(item => item.ProductVariantId == productVariantId);
        //}

        //Thêm sản phẩm vào giỏ hàng của người dùng
        public async Task<Cart> AddItemToCartAsync(int productVariantId, int? sizeId, int userId)
        {
            try
            {
                var productVariant = await _unitOfWork.ProductVariantRepository.GetByIdAsync(productVariantId);
                if (productVariant == null)
                {
                    //throw new Exception($"ProductVariant với id {productVariantId} không tồn tại.");
                    return null;
                }

                var variant = await _unitOfWork.ProductRepository.GetVariantWithSizesAsync(productVariantId);
                if (variant == null)
                    return null;

                var cart = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);
                var ps = variant.Sizes.FirstOrDefault(s => s.SizeId == sizeId.Value);
                var stock = ps.StockQuantity;

                //var cartItem = cart.Items.FirstOrDefault(i => i.ProductVariantId == productVariantId);
                var cartItem = cart.Items.FirstOrDefault(
                           i => i.ProductVariantId == productVariantId && i.SizeId == sizeId.Value);

                if (cartItem == null)
                {
                    // thêm mới 1 sp với quantity = 1 => kiểm tra đủ tồn kho không
                    if (stock < 1)
                        throw new InvalidOperationException("Hết hàng cho size này.");

                    cart.Items.Add(new CartItem
                    {
                        ProductVariantId = productVariantId,
                        Quantity = 1,
                        SizeId = sizeId.Value
                    });
                }
                else
                {
                    // tăng số lượng => kiểm tra tồn kho
                    var newQty = cartItem.Quantity + 1;
                    if (newQty > stock)
                        throw new InvalidOperationException(
                            $"Số lượng vượt tồn kho (tồn: {stock}) cho size {ps.Size.SizeType}."
                        );
                    cartItem.Quantity = newQty;
                }


                //if (cartItem != null)
                //{
                //    cartItem.Quantity += 1;
                //}
                //else
                //{
                //    cart.Items.Add(new CartItem { ProductVariantId = productVariantId, Quantity = 1, SizeId = sizeId.Value });
                //}

                await _unitOfWork.SaveChangesWithTransactionAsync();
                cart = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);

                return cart;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Cập nhật số lượng sản phẩm trong giỏ hàng
        //public async Task<Cart> UpdateProductQuantityInCartAsync(int productVariantId, int sizeId, int quantity, int userId)
        public async Task<Cart> UpdateProductQuantityInCartAsync(int cartItemId, int sizeId, int quantity, int userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);
            if (cart.UsersId != userId)
            {
                return null;
            }

            //var item = cart.Items.FirstOrDefault(i => i.ProductVariantId == productVariantId);
            var item = cart.Items.FirstOrDefault(i => i.Id == cartItemId);
            if (item == null)
            {
                return null;
            }

            var stock = item.ProductVariant.Sizes.FirstOrDefault(s => s.SizeId == sizeId).StockQuantity;

            if (quantity > stock)
            {
                throw new InvalidOperationException($"Số lượng vượt tồn kho (tồn: {stock}) " +
                    $"cho size {item.ProductVariant.Sizes.FirstOrDefault(s => s.SizeId == sizeId).Size.SizeType}");
            }

            item.Quantity = quantity;

            await _unitOfWork.CartRepository.UpdateAsync(cart);
            return cart;
        }

        ////Xóa sản phẩm khỏi giỏ hàng
        //public async Task<bool> RemoveProductFromCartAsync(int cartId, int userId)
        //{
        //    var cart = await _unitOfWork.CartRepository.GetByIdAsync(cartId);
        //    if (cart.UsersId != userId)
        //    {
        //        return false;
        //    }

        //    await _unitOfWork.CartRepository.RemoveAsync(cart);
        //    return true;
        //}

        public async Task<int> RemoveItemFromCart(int cartItemId, int userId)
        {
            try
            {
                var cart = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);
                if (cart.UsersId != userId)
                {
                    return 0;
                }
                var itemToRemove = cart.Items.FirstOrDefault(i => i.Id == cartItemId);
                if (itemToRemove == null)
                    return 0;

                cart.Items.Remove(itemToRemove);

                return await _unitOfWork.SaveChangesWithTransactionAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Order> CheckoutAsync(int userId, int paymentMethodId, string endAddress, string? discountCode)
        {
            var cart = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);
            if (cart == null || !cart.Items.Any())
                throw new InvalidOperationException("Giỏ hàng trống, không thể checkout.");

            foreach (var item in cart.Items)
            {
                var productSize = item.ProductVariant.Sizes.FirstOrDefault(s => s.SizeId == item.SizeId);

                if (item.Quantity > productSize.StockQuantity)
                    throw new InvalidOperationException($"Sản phẩm {item.ProductVariant.Product.Name}, size {productSize.Size.SizeType} không đủ hàng.");
            }

            decimal total = 0;
            foreach (var item in cart.Items)
            {
                total += item.ProductVariant.Product.Price * item.Quantity;
            }

            int now = (int)DateTimeOffset.Now.ToUnixTimeSeconds();


            var order = new Order
            {
                CreatedDate = now,
                Status = OrderStatusEnum.Pending.ToString(),
                Total = total,
                UserId = userId,
            };

            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductVariant = item.ProductVariant,
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity,
                    SizeType = item.Size.SizeType
                };

                order.Items.Add(orderItem);

                // Trừ tồn kho
                var productSize = item.ProductVariant.Sizes.FirstOrDefault(s => s.SizeId == item.SizeId);
                productSize.StockQuantity -= item.Quantity;
            }


            // Tạo Payment
            var payment = new Payment
            {
                Amount = total,
                CreatedDate = now,
                Status = PaymentStatusEnum.Pending.ToString(),
                PaymentMethodId = paymentMethodId,
            };

            order.Payments.Add(payment);

            var shipping = new Shipping
            {
                Status = ShippingStatusEnum.Pending.ToString(),
                CreatedDate = now,
                StartDate = now,
                FinishDate = (int)DateTimeOffset.Now.AddDays(3).ToUnixTimeSeconds(),
                StartAddress = "Lô E2a-7, Đường D1, Khu Công nghệ cao, Phường Tăng Nhơn Phú, TPHCM",
                EndAddress = endAddress
            };


            order.Shippings.Add(shipping);

            await _unitOfWork.OrderRepository.CreateAsync(order);
            await _unitOfWork.SaveChangesWithTransactionAsync();

            cart.Items.Clear();
            await _unitOfWork.SaveChangesWithTransactionAsync();
            return order;
        }

        //Xóa tất cả sản phẩm khỏi giỏ hàng của người dùng
        public async Task<int> ClearCartAsync(int userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);

            cart.Items.Clear();

            return await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
