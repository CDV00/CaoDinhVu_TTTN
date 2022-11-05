using Entities.Requests;
using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IProductColorService
    {
        Task<BaseResponse> AddAsync(ProductColorRequest productColorRequest);
        Task<BaseResponse> Delete(Guid id);
        List<Guid> GetIdByProductId(Guid productId);
        Guid GetProductColorId(Guid productId, Guid colorId);
        Task<BaseResponse> Update(ProductColorRequest productColorRequest);
    }
}
