using AutoMapper;
using Entities.Models;
using Repository.Repositories;
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
        public async Task<Category> getById(Guid id)
        {
            var category = await _categoryRepository.BuildQuery().FiterById(id)/*.CountAsync();//*/.AsSelectorAsync(c => _mapper.Map<Category>(c));
            //var category = _categoryRepository.GetAll().FirstOrDefault();
            return category;
        }
    }
}
