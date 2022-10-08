using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface ICategoryService
    {
        Task<Category> getById(Guid id);
    }
}
