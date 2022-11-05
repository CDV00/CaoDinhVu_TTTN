using Entities.Responses;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IMailService
    {
        Task<BaseResponse> SeedMail(string _from, string _to, string _toUser, string _subject, string _body);
    }

}
