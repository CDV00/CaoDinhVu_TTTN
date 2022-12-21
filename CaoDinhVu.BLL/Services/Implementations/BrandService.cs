using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Entities.Requests;
using Entities.Responses;
using Repository.Repositories;
using Repository.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class BrandService:IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IMapper mapper, IBrandRepository brandRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<BrandDTO>> getAll(int? status = 1)
        {
            try
            {
                var lisBrands = await _brandRepository.BuildQuery().FilterStatus(status.Value).ToListNoTrackingAsync(b => _mapper.Map<BrandDTO>(b));
                return lisBrands;
            }
            catch (Exception ex)
            {
                return null;
                throw new("Something went wrong. " + ex.Message);
            }
        }
        public bool CheckExist(Guid id)
        {
            try
            {
                return _brandRepository.CheckExists(id);
            }
            catch (Exception ex)
            {
                return false;
                throw new("Something went wrong. " + ex.Message);
            }
        }
        public async Task<BrandDTO> getById(Guid id)
        {
            try
            {
                var brand = await _brandRepository.BuildQuery().FindById(id).AsSelectorAsync(b => _mapper.Map<BrandDTO>(b));
                return brand;
            }
            catch (Exception ex)
            {
                return null;
                throw new("Something went wrong. " + ex.Message);
            }
        }
        public async Task<BaseResponse> AddAsync(BrandRequest brandRequest)
        {
            try
            {
                var brand = _mapper.Map<Brand>(brandRequest);
                await _brandRepository.CreateAsync(brand);
                _brandRepository.ChangeOrder(brand.Orders.Value);
                await _unitOfWork.SaveChangesAsync();


                return new BaseResponse(true, "Thêm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "Thêm thương hiệu thất bại " + ex);
            }
        }

        public async Task<BaseResponse> Update(BrandRequest brandRequest)
        {
            try
            {
                //int order = categoryRequest.Orders.Value;
                var brandOld = await _brandRepository.GetByIdAsync(brandRequest.Id.Value);
                var orderOld = brandOld.Orders.Value == null? brandRequest.Orders.Value: brandOld.Orders.Value;
                //categoryRequest.UpdateBy =categoryold.CreateBy;
                //categoryRequest.CreateBy =categoryold.CreateBy;
                //
                if (brandRequest.Image == null)
                    brandRequest.Image = brandOld.Image;
                if (brandRequest.ParentId == null)
                    brandRequest.ParentId = brandOld.ParentId;
                if (brandRequest.Slug == null)
                    brandRequest.Slug = brandOld.Slug;
                if (brandRequest.UpdateBy == null)
                    brandRequest.UpdateBy = brandOld.CreateBy;

                //
                if (brandRequest.Orders != orderOld)
                    _brandRepository.ChangeOrder(brandRequest.Orders.Value, orderOld);
                var brand = _mapper.Map(brandRequest, brandOld);
                //category.Tracking();            
                //check order exist

                brand.UpdateAt = DateTime.UtcNow;
                var update =await _brandRepository.Update(brand);
                if (!update)
                    return new BaseResponse(true, "Update thương hiệu thất bại");
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Update thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Update thương hiệu thất bại" + ex);
            }
        }
        public async Task<BaseResponse> ChangeStatus(Guid id)
        {
            try
            {
                var category = await _brandRepository.GetByIdAsync(id);
                if (category.Status == 1)
                    category.Status = 2;
                else
                    category.Status = 1;

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, category.Status.ToString());
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "thay đổi trạng thái thất bại" + ex);
            }
        }
        public async Task<BaseResponse> DeleteSoft(Guid id)
        {
            try
            {
                var brand = await _brandRepository.GetByIdAsync(id);
                if(brand.Status != 0)
                {
                    brand.Status = 0;
                }
                else
                {
                    brand.Status = 2;
                }
                

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "xóa mềm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "xóa mềm thất bại" + ex);
            }
        }
        public async Task<BaseResponse> Delete(Guid id)
        {
            try
            {
                var brand = await _brandRepository.GetByIdAsync(id);
                brand.IsDelete = true;
                
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Delete thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "Delete thất bại" + ex);
            }
        }

    }
}
