using Entities.Models;
using Entities.Responses;
using System;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IOrderDetailService
    {
        Task<BaseResponse> Add(OrderDetail orderDetail);
        Task<long> GetAmountInDay(DateTime dateTime);
    }
}
