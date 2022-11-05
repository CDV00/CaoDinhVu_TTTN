using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IColorService
    {
        Task<List<ColorDTO>> GetALL();
    }

}
