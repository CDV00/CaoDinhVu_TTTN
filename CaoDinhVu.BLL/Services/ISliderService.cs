using Entities.DTOs;
using Entities.Models;
using Entities.Requests;
using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface ISliderService
    {
        Task<BaseResponse> ChangeOrder(Guid id, string actionChange);
        Task<BaseResponse> ChangeStatus(Guid id);
        Task<BaseResponse> Create(SliderRequest sliderRequest);
        Task<BaseResponse> Delete(Guid id);
        Task<BaseResponse> DeleteSoft(Guid id);
        Task<List<SliderDTO>> getAll();
        Task<SliderDTO> GetById(Guid id);
        Task<BaseResponse> Update(Slider sliderRequest);
    }
}
