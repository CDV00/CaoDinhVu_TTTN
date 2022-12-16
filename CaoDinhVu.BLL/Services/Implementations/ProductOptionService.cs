using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository.Repositories;
using AutoMapper;
using Entities.DTOs;
using Entities.Requests;
using Entities.Responses;
using Repository.Repositories.Implementations;
using System.Linq;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class ProductOptionService : IProductOptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductOptionRepository _productOptionRepository;
        private readonly IProductRepository _productRepository;

        public ProductOptionService(IProductRepository productRepository,IUnitOfWork unitOfWork,IMapper mapper, IProductOptionRepository productOptionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productOptionRepository = productOptionRepository;
            _productRepository = productRepository;
        }

        public async Task<List<OptionDTO>> getOptionbyProductcolorId(Guid productColorId)
        {
            var productOption =await _productOptionRepository.BuildQuery()
                                                             .FilterByProductColorId(productColorId)
                                                             .FilterStatusActive()
                                                             .IncludeOption()
                                                             .ToListAsync(p => _mapper.Map<ProductOptionDTO>(p));
            //.AsSelectorAsync(p => _mapper.Map<ProductOptionDTO>(p));
            productOption.Where(m => m.Status == 1);
            List<OptionDTO> options = new List<OptionDTO>();

            for (int i = 0; i< productOption.Count; i++)
            {
                var option = productOption[i].Option;
                options.Add(option);
                
            }

            return options;

        }

        public async Task<ProductOptionCartItem> GetByProductColor(Guid productColorId, Guid optionId)
        {
            var productOption = await _productOptionRepository.BuildQuery()
                                                              .FilterByProductColorId(productColorId)
                                                              .FilterOption(optionId)
                                                              .IncludeOption()
                                                              .IncludeProductColor()
                                                              .IncludeColor()
                                                              .IncludeProduct()
                                                              .AsSelectorAsync(c => _mapper.Map<ProductOptionCartItem>(c));
            return productOption;
        }

        public async Task<decimal> GetPrice(Guid productColorId , Guid optionId)
        {
            try
            {
                decimal price = await _productOptionRepository.BuildQuery().FilterByProductColorId(productColorId).FilterOption(optionId).AsSelectorAsync(c => c.Price.Value);
                return price;
            }
            catch (Exception ex)
            {
                return 0;
                throw new Exception(ex.Message);
            }
            
        }
        public List<Guid> GetIdByProductId(Guid productId)
        {
            var listId = _productOptionRepository.BuildQuery().FilterProductId(productId);
            return listId;
        }
        public async Task<BaseResponse> AddAsync(ProductOptionRequest productOptionRequest)
        {
            try
            {
                if(await GetPrice(productOptionRequest.ProductColorId.Value, productOptionRequest.OptionId.Value) > 0)
                    return new BaseResponse(false, "Sản phẩm đã tồn tại");
                var productOption = _mapper.Map<ProductOption>(productOptionRequest);
                await _productOptionRepository.CreateAsync(productOption);
                var product = await _productRepository.GetByIdAsync(productOption.ProductId.Value);
                if (product.Price == 0 || product.Price > productOption.Price)
                    product.Price = productOption.Price.Value;
                product.Status = 1;
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Thêm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Thêm Option thất bại " + ex);
            }
        }

        public async Task<BaseResponse> UpdateAsync(ProductOptionRequest productOptionRequest)
        {
            try
            {
                var productOption = await _productOptionRepository.GetByIdAsync(productOptionRequest.Id.Value);
                productOption.Number = productOptionRequest.Number;
                productOption.Price = productOptionRequest.Price;
                productOption.OptionId = productOptionRequest.OptionId.Value;
                var product = await _productRepository.GetByIdAsync(productOption.ProductId.Value);
                if (product.Price == 0 || product.Price > productOption.Price)
                    product.Price = productOption.Price.Value;
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Update thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "Update Option thất bại" + ex);
            }
        }
        public async Task<BaseResponse> Delete(Guid id)
        {
            try
            {
                var productOption = await _productOptionRepository.GetByIdAsync(id);
                productOption.IsDelete = true;
                var update =await _productOptionRepository.Update(productOption);
                if (!update)
                    return new BaseResponse(true, "Delete Option thất bại");
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Delete thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Delete Option thất bại" + ex);
            }
        }
        public async Task<BaseResponse> ChangeStatus(Guid id)
        {
            try
            {
                var productOption = await _productOptionRepository.GetByIdAsync(id);
                //change status
                if (productOption.Status == 1)
                    productOption.Status = 2;
                else if(productOption.Status == 2)
                    productOption.Status = 1;

                var update = await _productOptionRepository.Update(productOption);
                if (!update)
                    return new BaseResponse(true, "Change status product Option thất bại");
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Change status product thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Change statu products Option thất bại" + ex);
            }
        }
    }
}
