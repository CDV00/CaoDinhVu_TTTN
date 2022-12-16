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
            try
            {
                var productColorId = _productColorRepository.BuildQuery().FilterProductColorId(productId, colorId);
                return productColorId;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
                throw new Exception(ex.Message);
            }
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
                if(!GetProductColorId(productColorRequest.ProductId, productColorRequest.ColorId).Equals(Guid.Empty))
                    return new BaseResponse(true, GetProductColorId(productColorRequest.ProductId, productColorRequest.ColorId).ToString());
                var productColor = _mapper.Map<ProductColor>(productColorRequest);
                await _productColorRepository.CreateAsync(productColor);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, productColor.Id.ToString());
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
                var productColor = await _productColorRepository.GetByIdAsync(productColorRequest.Id.Value);

                _mapper.Map(productColorRequest,productColor);


                //var update =await _productColorRepository.Update(productColor);
                /*if(!update)
                    return new BaseResponse(true, "Update Color thất bại");*/
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
                var update = await _productColorRepository.Update(productColor);
                if (!update)
                    return new BaseResponse(true, "Delete product color thất bại");
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Delete product color thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Delete product color thất bại" + ex);
            }
        }
        public async Task<BaseResponse> ChangeStatus(Guid id)
        {
            try
            {
                var productColor = await _productColorRepository.GetByIdAsync(id);
                //change status
                if (productColor.Status == 1)
                    productColor.Status = 2;
                else if (productColor.Status == 2)
                    productColor.Status = 1;

                var update = await _productColorRepository.Update(productColor);
                if (!update)
                    return new BaseResponse(true, "Change status product color thất bại");
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Change status product color thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Change statu products color thất bại" + ex);
            }
        }



    }
}
