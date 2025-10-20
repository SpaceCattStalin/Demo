using Repositories.Entities;
using Repositories.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        //Thêm sản phẩm mới
        public async Task<bool> AddProductAsync(Product product)
        {
            var numberProductValid = await _unitOfWork.ProductRepository.CreateAsync(product);
            return numberProductValid > 0;
        }
        //Cập nhật sản phẩm
        public async Task<bool> UpdateProductAsync(Product product)
        {
            var numberProductValid = await _unitOfWork.ProductRepository.UpdateAsync(product);
            return numberProductValid > 0;
        }
        //Xóa sản phẩm
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
    }
}
