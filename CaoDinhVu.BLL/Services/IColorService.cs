using Entities.DTOs;
using Entities.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IColorService
    {
        Task<BaseResponse> AddAsync(ColorDTO colorRequest);
        Task<List<ColorDTO>> GetALL();
    }

}
