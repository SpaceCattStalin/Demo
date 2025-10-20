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
    public class ShippingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShippingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Lấy tất cả thông tin vận chuyển của hệ thống
        public async Task<IEnumerable<Shipping>> GetAllShippingsAsync()
        {
            var shippings = await _unitOfWork.ShippingRepository.GetAllAsync();
            return shippings;
        }

        //Tạo mới thông tin vận chuyển cho đơn hàng
        public async Task<bool> CreateShippingForOrderAsync(int orderId, Shipping shippingInfo)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                return false;
            }
            shippingInfo.IdNavigation = order;
            shippingInfo.Status = ShippingStatusEnum.Pending.ToString();
            shippingInfo.StartDate = DateTime.Now;
            var numberShippingValid = await _unitOfWork.ShippingRepository.CreateAsync(shippingInfo);

            return numberShippingValid > 0;
        }

        //Cập nhật trạng thái "đang vận chuyển" cho đơn hàng
        public async Task<bool> UpdateShippingStatusAsync(int shippingId)
        {
            var shipping = await _unitOfWork.ShippingRepository.GetByIdAsync(shippingId);
            if (shipping == null)
            {
                return false;
            }
            shipping.Status = ShippingStatusEnum.Shipping.ToString();
            shipping.StartDate = DateTime.Now;
            var numberShippingValid = await _unitOfWork.ShippingRepository.UpdateAsync(shipping);

            return numberShippingValid > 0;
        }

        //Cập nhật trạng thái "đã giao hàng" cho đơn hàng
        public async Task<bool> MarkShippingAsDeliveredAsync(int shippingId)
        {
            var shipping = await _unitOfWork.ShippingRepository.GetByIdAsync(shippingId);
            if (shipping == null)
            {
                return false;
            }
            shipping.Status = Services.Utils.ShippingStatusEnum.Delivered.ToString();
            shipping.FinishDate = DateTime.Now;
            var numberShippingValid = await _unitOfWork.ShippingRepository.UpdateAsync(shipping);
            return numberShippingValid > 0;
        }

        //Cập nhật trang thái "vận chuyển thất bại" cho đơn hàng
        public async Task<bool> CancelShippingAsync(int shippingId)
        {
            var shipping = await _unitOfWork.ShippingRepository.GetByIdAsync(shippingId);
            if (shipping == null)
            {
                return false;
            }
            shipping.Status = Services.Utils.ShippingStatusEnum.Failed.ToString();
            _unitOfWork.ShippingRepository.Update(shipping);
            return true;
        }

        //Lấy thông tin vận chuyển theo shippingId
        public async Task<Shipping> GetShippingByIdAsync(int shippingId)
        {
            var shipping = await _unitOfWork.ShippingRepository.GetByIdAsync(shippingId);
            return shipping;
        }

        //Lấy thông tin vận chuyển theo shippingId
        public async Task<Shipping> GetShippingByOrderIdAsync(int shippingId)
        {
            var shipping = await _unitOfWork.ShippingRepository.GetByIdAsync(shippingId);
            return shipping;
        }

    }
}
