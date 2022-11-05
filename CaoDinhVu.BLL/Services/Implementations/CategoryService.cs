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
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository,IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> getById(Guid id)
        {
            try
            {
                var category = await _categoryRepository.BuildQuery().FiterById(id).AsSelectorAsync(c => _mapper.Map<CategoryDTO>(c) );
                //var category = _categoryRepository.GetAll().FirstOrDefault();
                return category;
            }
            catch (Exception ex)
            {
                return null;
                throw new("Something went wrong. " + ex.Message);
            }
        }
        public async Task<List<CategoryDTO>> getAll()
        {
            try
            {
                var category = await _categoryRepository.BuildQuery().ToListAsync(c=> _mapper.Map<CategoryDTO>(c));
                //var category = _categoryRepository.GetAll().FirstOrDefault();
                return category;
            }
            catch (Exception ex)
            {
                return null;
                throw new("Something went wrong. " + ex.Message);
            }
        }
        public async Task<BaseResponse> AddAsync(CategoryRequest categoryRequest)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryRequest);
                category.Status = 0;
                await _categoryRepository.CreateAsync(category);
                await _unitOfWork.SaveChangesAsync();


                return new BaseResponse(true, "Thêm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Thêm dang mục thất bại " + ex);
            }
        }

        public async Task<BaseResponse> Update(CategoryRequest categoryRequest)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryRequest);
                var update = _categoryRepository.Update(category);
                if (!update)
                    return new BaseResponse(true, "Update danh mục thất bại");
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Update thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Update danh mục thất bại" + ex);
            }
        }
        public async Task<BaseResponse> Delete(Guid id)
        {
            try
            {
                var product = await _categoryRepository.GetByIdAsync(id);
                product.IsDelete = true;
                var update = _categoryRepository.Update(product);
                if (!update)
                    return new BaseResponse(true, "Delete sản phẩm thất bại");
                await _unitOfWork.SaveChangesAsync();
                //var productColor = _productColorService.GetIdByProductId(id);
                return new BaseResponse(true, "Delete thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(true, "Delete sản phẩm thất bại" + ex);
            }
        }

    }
}
