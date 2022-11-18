using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Constants;
using Entities.Models;
using Entities.Requests;
using Entities.Responses;
using Repository.Repositories;
using Repository.Repositories.Implementations;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class ProductColorService:IProductColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductColorRepository _productColorRepository;
        private readonly IMapper _mapper;

        public ProductColorService(IUnitOfWork unitOfWork,IProductColorRepository productColorRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productColorRepository = productColorRepository;
            _mapper = mapper;
        }
        public Guid GetProductColorId(Guid productId, Guid colorId)
        {
            return _productColorRepository.BuildQuery().FilterProductColorId(productId, colorId);
        }
        public  List<Guid> GetIdByProductId(Guid productId)
        {
            var listId = _productColorRepository.BuildQuery().FilterProductId(productId);
            return listId;
        }
        public async Task<BaseResponse> AddAsync(ProductColorRequest productColorRequest)
        {
            try
            {
                var productColor = _mapper.Map<ProductColor>(productColorRequest);
                await _productColorRepository.CreateAsync(productColor);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Thêm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Thêm Color thất bại "+ ex);
            }
        }

        public async Task<BaseResponse> Update(ProductColorRequest productColorRequest)
        {
            try
            {
                var productColor = _mapper.Map<ProductColor>(productColorRequest);
                var update =await _productColorRepository.Update(productColor);
                if(!update)
                    return new BaseResponse(true, "Update Color thất bại");
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Update thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Update Color thất bại" + ex);
            }
        }
        public async Task<BaseResponse> Delete(Guid id)
        {
            try
            {
                var productColor = await _productColorRepository.GetByIdAsync(id);
                productColor.IsDelete = true;
                var update =await _productColorRepository.Update(productColor);
                if (!update)
                    return new BaseResponse(true, "Delete Color thất bại");
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Delete thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Delete Color thất bại" + ex);
            }
        }
    }
}
