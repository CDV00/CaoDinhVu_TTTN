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
        public async Task<List<BrandDTO>> getAll()
        {
            try
            {
                var lisBrands = await _brandRepository.BuildQuery().ToListAsync(b => _mapper.Map<BrandDTO>(b));
                return lisBrands;
            }
            catch (Exception ex)
            {
                return null;
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
                brand.Status = 0;
                await _brandRepository.CreateAsync(brand);
                await _unitOfWork.SaveChangesAsync();


                return new BaseResponse(true, "Thêm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Thêm thương hiệu thất bại " + ex);
            }
        }

        public async Task<BaseResponse> Update(BrandRequest brandRequest)
        {
            try
            {
                var brand = _mapper.Map<Brand>(brandRequest);
                var update = _brandRepository.Update(brand);
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
        public async Task<BaseResponse> Delete(Guid id)
        {
            try
            {
                var product = await _brandRepository.GetByIdAsync(id);
                product.IsDelete = true;
                var update = _brandRepository.Update(product);
                if (!update)
                    return new BaseResponse(true, "Delete thất bại");
                await _unitOfWork.SaveChangesAsync();
                //var productColor = _productColorService.GetIdByProductId(id);
                return new BaseResponse(true, "Delete thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Delete thất bại" + ex);
            }
        }

    }
}
