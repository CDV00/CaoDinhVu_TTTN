using Entities.DTOs;
using Entities.Requests;
using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IAccountService
    {
        Task<BaseResponse> Register(RegisterRequest registerRequest);
        Task<Response<UserDTO>> Login(LoginRequest loginRequest);
    }
}
