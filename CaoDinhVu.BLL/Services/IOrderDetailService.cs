using Entities.Models;
using Entities.Responses;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IOrderDetailService
    {
        Task<BaseResponse> Add(OrderDetail orderDetail);
    }
}
