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

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class ProductOptionService : IProductOptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductOptionRepository _productOptionRepository;

        public ProductOptionService(IUnitOfWork unitOfWork,IMapper mapper, IProductOptionRepository productOptionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productOptionRepository = productOptionRepository;
        }

        public async Task<List<OptionDTO>> getOptionbyProductcolorId(Guid productColorId)
        {
            var productOption =await _productOptionRepository.BuildQuery()
                                                             .FilterByProductColorId(productColorId)
                                                             .IncludeOption()
                                                             .ToListAsync(p => _mapper.Map<ProductOptionDTO>(p));
                                                             //.AsSelectorAsync(p => _mapper.Map<ProductOptionDTO>(p));

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
            decimal price = await _productOptionRepository.BuildQuery().FilterByProductColorId(productColorId).FilterOption(optionId).AsSelectorAsync(c => c.Price.Value);
            return price;
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
                var productOption = _mapper.Map<ProductOption>(productOptionRequest);
                await _productOptionRepository.CreateAsync(productOption);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Thêm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Thêm Option thất bại " + ex);
            }
        }

        public async Task<BaseResponse> Update(ProductOptionRequest productOptionRequest)
        {
            try
            {
                var productOption = _mapper.Map<ProductOption>(productOptionRequest);
                var update =await _productOptionRepository.Update(productOption);
                if (!update)
                    return new BaseResponse(true, "Update Option thất bại");
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Update thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Update Option thất bại" + ex);
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
    }
}
