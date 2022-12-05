using Entities.DTOs;
using Entities.Models;
using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IOptionService
    {
        Task<BaseResponse> AddAsync(OptionDTO optionRequest);
        Task<List<OptionDTO>> GetALL();
        Task<List<Option>> getbyProductcolorId(Guid id);
    }
}
