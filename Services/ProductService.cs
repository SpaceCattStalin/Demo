using AutoMapper;
using Repositories.Basic;
using Repositories.DTOs;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<PaginationResult<Product>> GetThumbnailProducts(
             DTOs.ProductFilterRequest filter)
        {
            var mappedFilter = _mapper.Map<Repositories.DTOs.ProductFilterRequest>(filter);

            var result = await _unitOfWork.ProductRepository
                .GetAllProductsByCategory(mappedFilter);

            return result;

            // Map entity → DTO
            //var dtoItems = _mapper.Map<List<ProductThumbnailDto>>(result.Items);

            //return new PaginationResult<Product>(
            //    result, result.CurrentPage, result.PageSize, result.TotalItems, result.TotalPages
            //);
        }

        public async Task<List<string>> GetIsPrimaryProductImage()
        {
            return await _unitOfWork.ProductRepository.GetPrimaryProductImages();
        }

        public async Task<int> DeleteProduct(int id)
        {
            return await _unitOfWork.ProductRepository.DeleteProduct(id);
        }

        public async Task<Product> GetProductWithVariants(int productId)
        {
            return await _unitOfWork.ProductRepository.GetProductWithVariantById(productId);
        }

        public async Task<PaginationResult<Product>> GetAllProductVariants(int currentPage, int pageSize)
        {
            return await _unitOfWork.ProductRepository.GetAllProductVariants(currentPage, pageSize);
        }

        public async Task<bool> AddProducts(CreateProductModel model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);

                int now = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                product.CreatedAt = now;
                product.Images = model.Images.Select(img => new ProductImage
                {
                    Url = img.Url,
                    CreatedAt = now,
                    ImageTypeId = img.ImageTypeId,
                    IsPrimary = img.IsPrimary,
                    SortOrder = img.SortOrder
                }).ToList();

                product.Variants = model.Variants.Select(v => new ProductVariant
                {
                    Color = v.Color,
                    CreatedAt = now,
                    //Size = v.Size,
                    //StockQuantity = v.StockQuantity,
                    VariantCode = v.VariantCode,
                    Images = v.Images.Select(img => new ProductImage
                    {
                        Url = img.Url,
                        CreatedAt = now,
                        ImageTypeId = img.ImageTypeId,
                        IsPrimary = img.IsPrimary,
                        SortOrder = img.SortOrder
                    }).ToList()
                }).ToList();

                await _unitOfWork.ProductRepository.CreateAsync(product);

                var rows = await _unitOfWork.SaveChangesWithTransactionAsync();

                return rows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateProduct(UpdateProductModel productModel)
        {
            try
            {
                var dao = _mapper.Map<UpdateProductDAO>(productModel);
                var updated = await _unitOfWork.ProductRepository.UpdateProductWithImage(dao);

                return updated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Size>> GetSizesByProductIdAsync(int productVariantId)
        {
            var product = await _unitOfWork.ProductRepository.GetProductWithVariantById(productVariantId);

            if (product == null)
                throw new KeyNotFoundException("Không tìm thấy sản phẩm");

            var sizes = product.Variants.SelectMany(v => v.Sizes)
                        .Select(ps => ps.Size)
                        .Distinct()
                        .ToList();

            return sizes;
        }
    }
}
