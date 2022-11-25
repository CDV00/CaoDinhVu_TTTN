using Entities.DTOs;
using Entities.Requests;
using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IBrandService
    {
        Task<BaseResponse> AddAsync(BrandRequest brandRequest);
        Task<BaseResponse> ChangeStatus(Guid id);
        bool CheckExist(Guid id);
        Task<BaseResponse> Delete(Guid id);
        Task<BaseResponse> DeleteSoft(Guid id);
        Task<List<BrandDTO>> getAll(int? status = 1);
        Task<BrandDTO> getById(Guid id);
        Task<BaseResponse> Update(BrandRequest brandRequest);
    }
}
