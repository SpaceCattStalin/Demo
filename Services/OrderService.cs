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
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Lấy tất cả thông tin đơn hàng của hệ thống 
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            return orders;
        }

        //Lấy thông tin đơn hàng theo OrderId
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            return order;
        }

        //Cập nhật đơn hàng
        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var numberValid = await _unitOfWork.OrderRepository.UpdateAsync(order);
            return numberValid > 0;
        }

        //Cập nhật trạng thái đơn hàng
        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                return false;
            }
            order.Status = status;
            order.UpdatedDate = DateTime.Now;
            var numberValid = await _unitOfWork.OrderRepository.UpdateAsync(order);
            return numberValid > 0;
        }

        //Lấy tất cả đơn hàng của người dùng theo userId
        public async Task<IEnumerable<Order>> GetAllOrdersByUserIdAsync(int userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
            return orders;
        }

        //Lấy tất cả đơn hàng của người dùng theo trạng thái pending
        public async Task<IEnumerable<Order>> GetPendingOrdersByUserIdAsync(int userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
            var pendingOrders = orders.Where(o => o.Status == OrderStatusEnum.Pending.ToString());
            return pendingOrders;
        }

        //Lấy tất cả đơn hàng của người dùng theo trạng thái đã hoàn thành
        public async Task<IEnumerable<Order>> GetCompletedOrdersByUserIdAsync(int userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
            var completedOrders = orders.Where(o => o.Status == OrderStatusEnum.Success.ToString());
            return completedOrders;
        }

        //Lấy tất cả đơn hàng của người dùng theo trạng thái đã hủy
        public async Task<IEnumerable<Order>> GetCancelledOrdersByUserIdAsync(int userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
            var cancelledOrders = orders.Where(o => o.Status == OrderStatusEnum.Cancelled.ToString());
            return cancelledOrders;
        }

        //Tạo đơn hàng từ giỏ hàng của người dùng
        public async Task<bool> CreateOrderFromCartAsync(int userId, List<int> listCart)
        {
            var carts = await _unitOfWork.CartRepository.GetCartsByUserIdAsync(userId);

            var selectedCarts = carts.Where(c => listCart.Contains(c.Id)).ToList();

            if (!selectedCarts.Any())
            {
                return false;
            }

            var order = new Order
            {
                UsersId = userId,
                Total = selectedCarts.Sum(c => c.Quantity * c.Product.Price),
                CreatedDate = DateTime.Now,
                Status = OrderStatusEnum.Pending.ToString()
            };
            var numberOrderValid = await _unitOfWork.OrderRepository.CreateAsync(order);
            var numberCartValid = 0;

            foreach (var cart in selectedCarts)
            {
                cart.OrdersId = order.Id;
                numberCartValid = await _unitOfWork.CartRepository.UpdateAsync(cart);
            }

            var payment = new Payment
            {
                IdNavigation = order,
                Amount = order.Total,
                Type = "Online",
                Status = PaymentStatusEnum.Pending.ToString(),
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            var numberPaymentValid = await _unitOfWork.PaymentRepository.CreateAsync(payment);

            return numberPaymentValid > 0 && numberOrderValid > 0 && numberCartValid > 0;
        }

        //Hủy đơn hàng theo orderId
        public async Task<bool> CancelOrderAsync(int orderId, int userId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

            if (order == null || order.UsersId != userId || order.Status != OrderStatusEnum.Pending.ToString())
            {
                return false;
            }

            order.Status = OrderStatusEnum.Cancelled.ToString();
            order.UpdatedDate = DateTime.Now;
            var numberValid = await _unitOfWork.OrderRepository.UpdateAsync(order);

            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(orderId);
            var numberPaymetValid = 0;
            if (payment != null)
            {
                payment.Status = PaymentStatusEnum.Cancelled.ToString();
                payment.UpdatedDate = DateTime.Now;
                numberPaymetValid = await _unitOfWork.PaymentRepository.UpdateAsync(payment);
            }

            return numberValid > 0 && numberPaymetValid > 0;
        }


    }
}
