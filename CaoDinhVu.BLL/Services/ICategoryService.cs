using Entities.DTOs;
using Entities.Requests;
using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface ICategoryService
    {
        Task<BaseResponse> AddAsync(CategoryRequest categoryRequest);
        Task<BaseResponse> Delete(Guid id);
        Task<List<CategoryDTO>> getAll();
        Task<CategoryDTO> getById(Guid id);
        Task<BaseResponse> Update(CategoryRequest categoryRequest);
    }
}
