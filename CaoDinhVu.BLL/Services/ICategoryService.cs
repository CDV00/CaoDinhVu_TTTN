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
        Task<BaseResponse> ChangeStatus(Guid id);
        bool CheckExist(Guid id);
        Task<BaseResponse> Delete(Guid id);
        Task<BaseResponse> DeleteSoft(Guid id);
        Task<List<CategoryDTO>> getAll(int? status = 1);
        Task<CategoryDTO> getById(Guid id);
        Task<BaseResponse> Update(CategoryRequest categoryRequest);
    }
}
