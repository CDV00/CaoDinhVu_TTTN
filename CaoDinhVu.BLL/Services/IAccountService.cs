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
        Task<Response<UserDTO>> GetById(Guid id);
        Task<IEnumerable<UserDTO>> GetAll();
        Task<Response<ProfileSetting>> GetProfileSetting(Guid id);
        Task<BaseResponse> ChangePassword(Guid userId, ChangePasswordRequest changePasswordRequest);
        Task<BaseResponse> UpdateProfile(UserRequest userRequest);
        Task<BaseResponse> ChangeStatus(Guid id);
        Task<BaseResponse> DeleteSoft(Guid id);
        Task<BaseResponse> Delete(Guid id);
    }
}
