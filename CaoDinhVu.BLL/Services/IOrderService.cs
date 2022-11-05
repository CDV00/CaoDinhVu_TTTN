using Entities.Requests;
using Entities.Responses;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IOrderService
    {
        Task<BaseResponse> Add(PaymentRequest paymentRequest);
    }
}
