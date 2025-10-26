using AutoMapper;
using Repositories.Basic;
using Repositories.Entities;
using Repositories.UnitOfWorks;
using Services.DTOs;
using Services.Interfaces;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    //public class ProductService : IProductService
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Lấy tất cả sản phẩm
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return products;
        }
        //Lấy sản phẩm theo productId
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            return product;
        }
        ////Thêm sản phẩm mới
        public async Task<bool> AddProductAsync(Product product)
        {
            var numberProductValid = await _unitOfWork.ProductRepository.CreateAsync(product);
            return numberProductValid > 0;
        }
        ////Cập nhật sản phẩm
        public async Task<bool> UpdateProductAsync(Product product)
        {
            var numberProductValid = await _unitOfWork.ProductRepository.UpdateAsync(product);
            return numberProductValid > 0;
        }
        ////Xóa sản phẩm
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var numberProductValid = false;
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product != null)
            {
                numberProductValid = await _unitOfWork.ProductRepository.RemoveAsync(product);
            }
            return numberProductValid;
        }

        public async Task<PaginationResult<ProductThumbnailDto>> GetThumbnailProducts(
             int categoryId, int currentPage, int pageSize)
        {
            var result = await _unitOfWork.ProductRepository
                .GetAllProductsByCategory(categoryId, currentPage, pageSize);

            // Map entity → DTO
            var dtoItems = _mapper.Map<List<ProductThumbnailDto>>(result.Items);

            return new PaginationResult<ProductThumbnailDto>(
                dtoItems, result.CurrentPage, result.PageSize, result.TotalItems
            );
        }
    }
}
