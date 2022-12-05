using Entities.DTOs;
using Entities.Requests;
using Entities.Responses;
using System;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IOrderService
    {
        Task<BaseResponse> Add(PaymentRequest paymentRequest);
        Task<Responses<OrderDTO>> GetAll(int? status = 1);
        Task<Response<OrderDTO>> GetDetailById(Guid id);
        Task<Responses<OrderDTO>> GetByUserId(Guid userId);
        Responses<OrderByBrandResponse> GetOrderByBrand();
        Responses<OrderByCategoryResponse> GetOrderByCategory();
        Responses<OrderInWeekResponse> GetOrderInWeek();
        Task<BaseResponse> DeleteSoft(Guid id);
        Task<BaseResponse> ChangeStatus(Guid id);
        Task<int> CountOrderByStatus(Guid userId,int? status = 4);
    }
}
