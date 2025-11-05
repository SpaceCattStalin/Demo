using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.DTOs;
using Repositories.Entities;
using Repositories.UnitOfWorks;
using Services.DTOs;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Lấy tất cả thông tin đơn hàng của hệ thống 
        public async Task<PaginationResult<Order>> GetAllOrdersAsync(DTOs.AdminOrderFilterRequest filter)
        {
            var mappedFilter = _mapper.Map<Repositories.DTOs.AdminOrderFilterRequest>(filter);

            var orders = await _unitOfWork.OrderRepository.GetAllOrders(mappedFilter);
            return orders;
        }

        //Lấy thông tin đơn hàng theo OrderId
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            return order;
        }
        public async Task<Order?> GetOrderDetailAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderDetailByIdAsync(orderId);
            if (order == null)
                throw new InvalidOperationException($"Không tìm thấy đơn hàng với ID = {orderId}");

            return order;
        }

        //Lấy thông tin order của 1 user 
        public async Task<PaginationResult<Order>> GetOrderByUser(int userId, DTOs.UserOrderFilterRequest filter)
        {
            var mappedFilter = _mapper.Map<Repositories.DTOs.UserOrderFilterRequest>(filter);
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId, mappedFilter);

            return orders;
        }

        //Cập nhật đơn hàng
        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var numberValid = await _unitOfWork.OrderRepository.UpdateAsync(order);
            return numberValid > 0;
        }

        //Cập nhật trạng thái đơn hàng
        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatusEnum status)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                return false;
            }
            order.Status = status.ToString();
            order.UpdatedDate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var numberValid = await _unitOfWork.OrderRepository.UpdateAsync(order);
            return numberValid > 0;
        }

        private async Task<ProductVariant?> GetVariantWithSizesAndProductAsync(int variantId)
        {
            return await _unitOfWork.ProductVariantRepository.GetByAsync(
                pv => pv.Id == variantId,
                q => q.Include(pv => pv.Sizes)
                      .Include(pv => pv.Product)
            );
        }

        public async Task<bool> AddProductToOrderAsync(int orderId, AddOrderItemDTO addOrderItem)
        {
            var order = await _unitOfWork.OrderRepository.GetByAsync(
                o => o.Id == orderId,
                q => q.Include(o => o.Items)
                      .ThenInclude(i => i.ProductVariant)
                          .ThenInclude(pv => pv.Sizes)
                      .Include(o => o.Items)
                      .ThenInclude(i => i.ProductVariant)
                          .ThenInclude(pv => pv.Product)
            );
            if (order == null) return false;

            var existingItem = order.Items.FirstOrDefault(i =>
                i.ProductVariantId == addOrderItem.ProductVariantId &&
                i.SizeId == addOrderItem.SizeId
            );

            ProductVariant variant;
            if (existingItem != null)
                variant = existingItem.ProductVariant;
            else
            {
                variant = await GetVariantWithSizesAndProductAsync(addOrderItem.ProductVariantId);
                if (variant == null) return false;
            }

            int availableStock;
            if (addOrderItem.SizeId == null)
            {
                if (!variant.StockQuantity.HasValue) return false;
                availableStock = variant.StockQuantity.Value;
            }
            else
            {
                var size = variant.Sizes.FirstOrDefault(s => s.Id == addOrderItem.SizeId && s.ProductVariantId == variant.Id);
                if (size == null) return false;
                availableStock = size.StockQuantity;
            }

            if (addOrderItem.Quantity <= 0 || addOrderItem.Quantity > availableStock)
                return false;

            if (addOrderItem.SizeId == null)
                variant.StockQuantity -= addOrderItem.Quantity;
            else
            {
                var size = variant.Sizes.First(s => s.Id == addOrderItem.SizeId && s.ProductVariantId == variant.Id);
                size.StockQuantity -= addOrderItem.Quantity;
            }

            if (existingItem != null)
                existingItem.Quantity += addOrderItem.Quantity;
            else
            {
                var newItem = new OrderItem
                {
                    ProductVariantId = addOrderItem.ProductVariantId,
                    SizeId = addOrderItem.SizeId,
                    Quantity = addOrderItem.Quantity,
                    OrderId = order.Id,
                    ProductVariant = variant
                };
                order.Items.Add(newItem);
            }

            order.Total = Math.Round(order.Items.Sum(i => i.Quantity * i.ProductVariant.Product.Price), 2);

            await _unitOfWork.SaveChangesWithTransactionAsync();
            return true;
        }

        public async Task<bool> RemoveProductFromOrderAsync(int orderId, RemoveOrderItemDTO removeOrderItem)
        {
            var order = await _unitOfWork.OrderRepository.GetByAsync(
                o => o.Id == orderId,
                q => q.Include(o => o.Items)
                      .ThenInclude(i => i.ProductVariant)
                          .ThenInclude(pv => pv.Sizes)
                      .Include(o => o.Items)
                      .ThenInclude(i => i.ProductVariant)
                          .ThenInclude(pv => pv.Product)
            );
            if (order == null) return false;

            var item = order.Items.FirstOrDefault(i =>
                i.ProductVariantId == removeOrderItem.ProductVariantId &&
                i.SizeId == removeOrderItem.SizeId
            );
            if (item == null) return false;

            var variant = item.ProductVariant;

            if (item.SizeId == null)
            {
                if (!variant.StockQuantity.HasValue) return false;
                variant.StockQuantity += item.Quantity;
            }
            else
            {
                var size = variant.Sizes.FirstOrDefault(s => s.Id == item.SizeId && s.ProductVariantId == variant.Id);
                if (size == null) return false;
                size.StockQuantity += item.Quantity;
            }

            order.Items.Remove(item);
            order.Total = Math.Round(order.Items.Sum(i => i.Quantity * i.ProductVariant.Product.Price), 2);

            await _unitOfWork.SaveChangesWithTransactionAsync();
            return true;
        }

        public async Task<bool> UpdateProductQuantityInOrderAsync(int orderId, UpdateOrderItemQuantityDTO updateOrderItemQuantity)
        {
            var order = await _unitOfWork.OrderRepository.GetByAsync(
                o => o.Id == orderId,
                q => q.Include(o => o.Items)
                      .ThenInclude(i => i.ProductVariant)
                          .ThenInclude(pv => pv.Sizes)
                      .Include(o => o.Items)
                      .ThenInclude(i => i.ProductVariant)
                          .ThenInclude(pv => pv.Product)
            );
            if (order == null) return false;

            var item = order.Items.FirstOrDefault(i =>
                i.ProductVariantId == updateOrderItemQuantity.ProductVariantId &&
                i.SizeId == updateOrderItemQuantity.SizeId
            );
            if (item == null) return false;

            var variant = item.ProductVariant;

            int availableStock;
            if (item.SizeId == null)
            {
                if (!variant.StockQuantity.HasValue) return false;
                availableStock = variant.StockQuantity.Value;
            }
            else
            {
                var size = variant.Sizes.FirstOrDefault(s => s.Id == item.SizeId && s.ProductVariantId == variant.Id);
                if (size == null) return false;
                availableStock = size.StockQuantity;
            }

            if (updateOrderItemQuantity.NewQuantity <= 0 || updateOrderItemQuantity.NewQuantity > (availableStock + item.Quantity))
                return false;

            int change = updateOrderItemQuantity.NewQuantity - item.Quantity;
            if (item.SizeId == null)
                variant.StockQuantity -= change;
            else
            {
                var size = variant.Sizes.First(s => s.Id == item.SizeId && s.ProductVariantId == variant.Id);
                size.StockQuantity -= change;
            }

            item.Quantity = updateOrderItemQuantity.NewQuantity;
            order.Total = Math.Round(order.Items.Sum(i => i.Quantity * i.ProductVariant.Product.Price), 2);

            await _unitOfWork.SaveChangesWithTransactionAsync();
            return true;
        }



        //Lấy tất cả đơn hàng của người dùng theo userId
        //public async Task<IEnumerable<Order>> GetAllOrdersByUserIdAsync(int userId)
        //{
        //    var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
        //    return orders;
        //}

        ////Lấy tất cả đơn hàng của người dùng theo trạng thái pending
        //public async Task<IEnumerable<Order>> GetPendingOrdersByUserIdAsync(int userId)
        //{
        //    var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
        //    var pendingOrders = orders.Where(o => o.Status == OrderStatusEnum.Pending.ToString());
        //    return pendingOrders;
        //}

        ////Lấy tất cả đơn hàng của người dùng theo trạng thái đã hoàn thành
        //public async Task<IEnumerable<Order>> GetCompletedOrdersByUserIdAsync(int userId)
        //{
        //    var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
        //    var completedOrders = orders.Where(o => o.Status == OrderStatusEnum.Success.ToString());
        //    return completedOrders;
        //}

        ////Lấy tất cả đơn hàng của người dùng theo trạng thái đã hủy
        //public async Task<IEnumerable<Order>> GetCancelledOrdersByUserIdAsync(int userId)
        //{
        //    var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
        //    var cancelledOrders = orders.Where(o => o.Status == OrderStatusEnum.Cancelled.ToString());
        //    return cancelledOrders;
        //}

        //Tạo đơn hàng từ giỏ hàng của người dùng
        //public async Task<bool> CreateOrderFromCartAsync(int userId, List<int> listCart)
        //{
        //    var carts = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);

        //    var selectedCarts = carts.Where(c => listCart.Contains(c.Id)).ToList();

        //    if (!selectedCarts.Any())
        //    {
        //        return false;
        //    }

        //    var order = new Order
        //    {
        //        UsersId = userId,
        //        Total = selectedCarts.Sum(c => c.Quantity * c.Product.Price),
        //        CreatedDate = DateTime.Now,
        //        Status = OrderStatusEnum.Pending.ToString()
        //    };
        //    var numberOrderValid = await _unitOfWork.OrderRepository.CreateAsync(order);
        //    var numberCartValid = 0;

        //    foreach (var cart in selectedCarts)
        //    {
        //        cart.OrdersId = order.Id;
        //        numberCartValid = await _unitOfWork.CartRepository.UpdateAsync(cart);
        //    }

        //    var payment = new Payment
        //    {
        //        IdNavigation = order,
        //        Amount = order.Total,
        //        Type = "Online",
        //        Status = PaymentStatusEnum.Pending.ToString(),
        //        CreatedDate = DateTime.Now,
        //        IsDeleted = false
        //    };
        //    var numberPaymentValid = await _unitOfWork.PaymentRepository.CreateAsync(payment);

        //    return numberPaymentValid > 0 && numberOrderValid > 0 && numberCartValid > 0;
        //}

        //Hủy đơn hàng theo orderId
        //public async Task<bool> CancelOrderAsync(int orderId, int userId)
        //{
        //    var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

        //    if (order == null || order.UsersId != userId || order.Status != OrderStatusEnum.Pending.ToString())
        //    {
        //        return false;
        //    }

        //    order.Status = OrderStatusEnum.Cancelled.ToString();
        //    order.UpdatedDate = DateTime.Now;
        //    var numberValid = await _unitOfWork.OrderRepository.UpdateAsync(order);

        //    var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(orderId);
        //    var numberPaymetValid = 0;
        //    if (payment != null)
        //    {
        //        payment.Status = PaymentStatusEnum.Cancelled.ToString();
        //        payment.UpdatedDate = DateTime.Now;
        //        numberPaymetValid = await _unitOfWork.PaymentRepository.UpdateAsync(payment);
        //    }

        //    return numberValid > 0 && numberPaymetValid > 0;
        //}


    }
}
