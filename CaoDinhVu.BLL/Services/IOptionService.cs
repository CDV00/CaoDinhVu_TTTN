using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IOptionService
    {
        Task<List<OptionDTO>> GetALL();
        Task<List<Option>> getbyProductcolorId(Guid id);
    }
}
