using AutoMapper;
using Repositories.Basic;
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
    public class ShippingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ShippingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Lấy danh sách shipping có lọc, phân trang
        public async Task<PaginationResult<Shipping>> GetAllShippingsAsync(AdminOrderFilterRequest filter)
        {
            var mappedFilter = _mapper.Map<Repositories.DTOs.AdminOrderFilterRequest>(filter);

            return await _unitOfWork.ShippingRepository.GetAllWithFilter(mappedFilter);
        }

        // Xem chi tiết shipping
        public async Task<Shipping?> GetShippingDetailAsync(int id)
        {
            return await _unitOfWork.ShippingRepository.GetDetailAsync(id);
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
            int now = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

            shippingInfo.Order = order;
            shippingInfo.Status = ShippingStatusEnum.Pending.ToString();
            shippingInfo.StartDate = now;
            var numberShippingValid = await _unitOfWork.ShippingRepository.CreateAsync(shippingInfo);

            return numberShippingValid > 0;
        }

        //Cập nhật trạng thái "đang vận chuyển" cho đơn hàng
        public async Task<bool> UpdateShippingStatusAsync(int shippingId)
        {
            int now = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

            var shipping = await _unitOfWork.ShippingRepository.GetByIdAsync(shippingId);
            if (shipping == null)
            {
                return false;
            }
            shipping.Status = ShippingStatusEnum.Shipping.ToString();
            shipping.StartDate = now;
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
            int now = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

            shipping.Status = Services.Utils.ShippingStatusEnum.Delivered.ToString();
            shipping.FinishDate = now;
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
