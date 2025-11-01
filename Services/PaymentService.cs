using Repositories.Basic;
using Repositories.DTOs;
using Repositories.DTOs.Services.DTOs;
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
    public class PaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Lấy tất cả payments cho admin
        public async Task<PaginationResult<Payment>> GetAllPayments(AdminPaymentFilterRequest filter)
        {
            try
            {
                return await _unitOfWork.PaymentRepository.GetAllPayments(filter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Payment?> GetPaymentById(int id)
        {
            try
            {
                return await _unitOfWork.PaymentRepository.GetPaymentById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdatePaymentStatus(int paymentId, PaymentStatusEnum paymentStatusEnum)
        {
            var existingPayment = await _unitOfWork.PaymentRepository.GetByIdAsync(paymentId);

            if (existingPayment == null)
            {
                throw new Exception($"Không tìm thấy payment với id {paymentId}");
            }

            existingPayment.Status = paymentStatusEnum.ToString();

            await _unitOfWork.PaymentRepository.UpdateAsync(existingPayment);

            return true;
        }
        public async Task<AdminPaymentStatisticDto> GetPaymentStatisticsAsync(int? fromDate = null, int? toDate = null)
        {
            return await _unitOfWork.PaymentRepository.GetPaymentStatisticsAsync(fromDate, toDate);
        }

        //Lấy thông tin tất cả thanh toán theo UserId
        //public async Task<IEnumerable<Payment>> GetAllPaymentsByUserIdAsync(int userId)
        //{
        //    var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
        //    var payments = new List<Payment>();
        //    foreach (var order in orders)
        //    {
        //        var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(order.Id);
        //        if (payment != null)
        //        {
        //            payments.Add(payment);
        //        }
        //    }
        //    return payments;
        //}

        //Lấy thông tin thanh toán đang chờ xử lý theo UserId
        //public async Task<IEnumerable<Payment>> GetPendingPaymentsByUserIdAsync(int userId)
        //{
        //    var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
        //    var payments = new List<Payment>();
        //    foreach (var order in orders)
        //    {
        //        var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(order.Id);
        //        if (payment != null && payment.Status == PaymentStatusEnum.Pending.ToString())
        //        {
        //            payments.Add(payment);
        //        }
        //    }
        //    return payments;
        //}

        //Lấy thông tin thanh toán thành công theo UserId
        //public async Task<IEnumerable<Payment>> GetSuccessfulPaymentsByUserIdAsync(int userId)
        //{
        //    var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
        //    var payments = new List<Payment>();
        //    foreach (var order in orders)
        //    {
        //        var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(order.Id);
        //        if (payment != null && payment.Status == PaymentStatusEnum.Success.ToString())
        //        {
        //            payments.Add(payment);
        //        }
        //    }
        //    return payments;
        //}

        //Lấy thông tin thanh toán thất bại theo UserId
        //public async Task<IEnumerable<Payment>> GetFailedPaymentsByUserIdAsync(int userId)
        //{
        //    var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
        //    var payments = new List<Payment>();
        //    foreach (var order in orders)
        //    {
        //        var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(order.Id);
        //        if (payment != null && payment.Status == PaymentStatusEnum.Failed.ToString())
        //        {
        //            payments.Add(payment);
        //        }
        //    }
        //    return payments;
        //}

        //Ghi nhận thanh toán thành công theo OrderId
        //public async Task<bool> RecordPaymentSuccessAsync(int orderId)
        //{
        //    var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
        //    if (order == null)
        //    {
        //        return false;
        //    }
        //    order.Status = OrderStatusEnum.Processing.ToString();
        //    order.UpdatedDate = DateTime.Now;
        //    var numberOrderValid = await _unitOfWork.OrderRepository.UpdateAsync(order);

        //    var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(orderId);
        //    payment.Status = PaymentStatusEnum.Success.ToString();
        //    payment.UpdatedDate = DateTime.Now;
        //    var numberPaymentValid = await _unitOfWork.PaymentRepository.UpdateAsync(payment);

        //    var shippingInfo = new Shipping();
        //    shippingInfo.IdNavigation = order;
        //    shippingInfo.Status = ShippingStatusEnum.Pending.ToString();
        //    shippingInfo.StartDate = DateTime.Now;
        //    var numberShippingValid = await _unitOfWork.ShippingRepository.CreateAsync(shippingInfo);

        //    var productsInOrder = order.Carts;

        //    var numberProductValid = 0;

        //    //foreach (var item in productsInOrder)
        //    //{
        //    //    var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
        //    //    if (product != null)
        //    //    {
        //    //        product.StockQuantity -= item.Quantity;
        //    //        if (product.StockQuantity <= 0)
        //    //        {
        //    //            product.IsAvailable = false;
        //    //        }
        //    //        numberOrderValid = await _unitOfWork.ProductRepository.UpdateAsync(product);
        //    //    }
        //    //}

        //    return numberOrderValid > 0 && numberPaymentValid > 0 && numberShippingValid > 0 && numberProductValid > 0;
        //}

        //Ghi nhận thanh toán thất bại theo OrderId
        //public async Task<bool> RecordPaymentFailureAsync(int orderId)
        //{
        //    var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
        //    if (order == null)
        //    {
        //        return false;
        //    }
        //    var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(orderId);
        //    payment.Status = PaymentStatusEnum.Failed.ToString();
        //    //payment.UpdatedDate = DateTime.Now;
        //    var numberPaymentValid = await _unitOfWork.PaymentRepository.CreateAsync(payment);

        //    return numberPaymentValid > 0;
        //}

    }
}
