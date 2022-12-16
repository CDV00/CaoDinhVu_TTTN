using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Entities.Requests;
using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IMailService _mailService;

        public AccountService(UserManager<AppUser> userManager, IMapper mapper, IUserRepository userRepository, RoleManager<IdentityRole<Guid>> roleManager, IMailService mailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            try
            {
                var users =await _userRepository.BuildQuery().ToListNoTrackingAsync(u=> _mapper.Map<UserDTO>(u));
                return users;
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.Message);
            }
        }
        public int GetRole(Guid id)
        {
            try
            {
               return _userRepository.checkRole(id);
            }
            catch (Exception ex)
            {

                throw new Exception("Lỗi: "+ex.Message);
            }
        }

        public async Task<Response<UserDTO>> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (user is null)
                    return new Response<UserDTO>(false, "Tài khoản không tồn tại!");

                /*if (!user.IsActive)
                    return new Response<LoginDTO>(false, "Authentication failed. Account has been blocked.", "403");*/
                //var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (!await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                {
                    return new Response<UserDTO>(false, "Tài khoản không tồn tại!");
                }
                var userResponse = _mapper.Map<UserDTO>(user);

                var roles = await _userManager.GetRolesAsync(user);
                userResponse.Role = GetRole(userResponse.Id.Value); //string.Join(",", roles);

                //_mapper.Map(user, UserDTO);
                /*var token = await CreateToken(populateExp: true);
                if (token == null)
                    return new Response<LoginDTO>(false, "Authentication Error. User don't have any role, please create new account!", null);
*/
                return new Response<UserDTO>(true,"Login Thành công", userResponse);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<BaseResponse> Register(RegisterRequest registerRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(registerRequest.Email);

                //check Email Confirmed
                //if()
                IdentityResult result;
                if (user != null ) 
                {
                    if (user.EmailConfirmed)
                    {
                        return new BaseResponse(false, "Email đã đăng ký tài khoản!");
                    }

                    GetAvartarUser(ref user);
                    user.UpdateAt = DateTime.Now;

                    await _userManager.AddPasswordAsync(user, registerRequest.Password);
                    result = await _userManager.UpdateAsync(user);


                    


                }
                else
                {
                    //var users = new AppUser();
                    //users = _mapper.Map<AppUser>(registerRequest);
                    //user = users;
                    //GetAvartarUser(user);
                    //user.CreateAt = DateTime.Now;

                    
                    var users = _mapper.Map<AppUser>(registerRequest);
                    users.Description = users.Description;
                    user = users;
                    users.CreateAt = DateTime.Now;
                    GetAvartarUser(ref user);
                    await _userManager.CreateAsync(users);
                    await _userManager.AddPasswordAsync(users, registerRequest.Password);
                    result = await _userManager.UpdateAsync(users);


                    //result = await _userManager.CreateAsync(user, registerRequest.Password);
                }

                if (!result.Succeeded)
                {
                    return new BaseResponse(false,"Lỗi! Vui lòng thử lại!");
                }
                //user.Description = registerRequest.Description;


                //_mapper.Map(registerRequest, user);
                //user.Description = registerRequest.Description;


                //await AddStudentRole(user);
                if (registerRequest.Role == UserRoles.Seller)
                {
                    await AddSalerRole(user);
                }
                else
                {
                    await AddCustomerRole(user);
                }



                //var userResponse = _mapper.Map<UserDTO>(user);

                //var roles = await _userManager.GetRolesAsync(user);
                //userResponse.Role = string.Join(",", roles);



                return new BaseResponse(true, "Đăng ký tài khoản thành công!");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }


        }



        private static void GetAvartarUser(ref AppUser user)
        {
            user.Fullname = user.Fullname.Trim();
            string FirstLetter = user.Fullname.Split()[0][0].ToString();
            var lastIndex = user.Fullname.Split().Length - 1;
            string LastLetter = null;
            if (lastIndex != 0)
                LastLetter = user.Fullname.Split()[lastIndex][0].ToString();

            var avartarUrl = "https://i2.wp.com/ui-avatars.com/api/" + FirstLetter + lastIndex + "/300/ed2a26/fff";
            user.AvatarUrl = avartarUrl;
        }

        private async Task AddSalerRole(AppUser user)
        {
            var salerRole = UserRoles.Seller;

            bool existRole = await _roleManager.RoleExistsAsync(salerRole);

            if (!existRole)
            {
                var role = new IdentityRole<Guid>();
                role.Name = salerRole;
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, salerRole);
        }
        private async Task AddCustomerRole(AppUser user)
        {
            var customerRole = UserRoles.Customer;

            bool existRole = await _roleManager.RoleExistsAsync(customerRole);

            if (!existRole)
            {
                var role = new IdentityRole<Guid>();
                role.Name = customerRole;
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, customerRole);
        }

        public async Task<Response<ProfileAddress>> GetProfileAddress(Guid userId)
        {
            try
            {
                //string s = userId.ToString();
                ProfileAddress profileAddress = new ProfileAddress();
                //int userId = Int32.Parse(Session["UserId"].ToString());
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if(user == null)
                {
                    return new Response<ProfileAddress>(false, "Thất bại");
                }
                _mapper.Map(user, profileAddress.AppUser);
                string address = profileAddress.AppUser.Address;

                /*int index;
                int length = address.Length;
                index =  address.LastIndexOf(",");*/
                int index = address.LastIndexOf(",");
                string s = address.Substring(index + 1);
                address.Remove(10);
                profileAddress.tinh = XuLyChuoi(ref address);
                profileAddress.huyen = XuLyChuoi(ref address);
                profileAddress.phuong = XuLyChuoi(ref address);
                profileAddress.chiTiet = address;
                return new Response<ProfileAddress>(true,"Thành công", profileAddress);
            }
            catch (Exception ex)
            {
                return new Response<ProfileAddress>(false, "Thất bại"+ex.Message);
                throw;
            }
        }

        public async Task<Response<ProfileSetting>> GetProfileSetting(Guid id)
        {
            try
            {
                ProfileSetting profileSetting = new ProfileSetting();
                profileSetting.AppUser = _mapper.Map<UserDTO>(await _userManager.FindByIdAsync(id.ToString()));
                profileSetting.AppUser.Role = _userRepository.checkRole(profileSetting.AppUser.Id.Value);
                profileSetting.Id = profileSetting.AppUser.Id;
                string address = profileSetting.AppUser.Address;
                profileSetting.Province = XuLyChuoi(ref address);
                profileSetting.District = XuLyChuoi(ref address);
                profileSetting.wards = XuLyChuoi(ref address);
                profileSetting.SpecificAddress = address;
                return new Response<ProfileSetting>(true,"thanh cong", profileSetting);
            }
            catch (Exception ex)
            {
                return new Response<ProfileSetting>(false, "lỗi " + ex.Message);
                throw;
            }
        }

        private String XuLyChuoi(ref string address)
        {
            int index = address.LastIndexOf(",");
            string s = address.Substring(index + 2);
            address = address.Remove(index);
            return s;
        }

        public async Task<Response<UserDTO>> GetById(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                return new Response<UserDTO>(true,"thành công", _mapper.Map<UserDTO>(user));
            }
            catch (Exception ex)
            {
                return new Response<UserDTO>(true, "thất bại: "+ex.Message);
            }
        }
        public async Task<Responses<UserDTO>> GetAll(Guid id)
        {
            try
            {
                var users = _userRepository.GetAll();
                return new Responses<UserDTO>(true, "thành công", _mapper.Map<List<UserDTO>>(users));
            }
            catch (Exception ex)
            {
                return new Responses<UserDTO>(true, "thất bại: " + ex.Message);
            }
        }
        public async Task<BaseResponse> ChangePassword(Guid userId, ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                    return new BaseResponse(false, "can't find user");
                var checkPassword = _userManager.CheckPasswordAsync(user, changePasswordRequest.PasswordOld);

                if (!checkPassword.Result)
                {
                    return new BaseResponse(false, "incorrect password!");
                }
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, changePasswordRequest.PasswordNew);
                await _userManager.UpdateAsync(user);
                return new Response<UserDTO>(true, null, null);
            }
            catch (Exception ex)
            {
                return new Responses<UserDTO>(false, ex.Message, null);
            }
        }
        //forget password
        public async Task<BaseResponse> ForgetPassWord(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response<BaseResponse>(false, "No user associated with email", null);
            }

            string subject = "Quên mật khẩu";
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = "https://localhost:5001/quen-mat-khau?email=" + user.Email + "&token=" + token;
            string body = "<h2>Quên mật khẩu</h2><br><p>Vui lòng click vào link: "+ link +"</p>";
            
            
            var sen = await _mailService.SeedMail("vucao005@gmail.com", user.Email,user.Fullname, subject, body);
            if (!sen.IsSuccess)
            {
                return new Response<BaseResponse>(false, "Send Email went wrong!" + sen.Message);
            }
            return new BaseResponse(true, "Thành công");
        }
        //
        /*public async Task<Response<BaseResponse>> ResetPassWord(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordRequest.email);
                if (user == null)
                {
                    return new Response<BaseResponse>(false, "No user associated with email", null);
                }
                if (user.CodeNumber is null)
                {
                    return new Response<BaseResponse>(false, "Something went wrong!", null);
                }
                if (user.CodeNumber != resetPasswordRequest.codeNumber || DateTime.Now >= user.UpdatedAt.Value.AddMinutes(3))
                {
                    return new Response<BaseResponse>(false, "Something went wrong!", null);
                }
                if (resetPasswordRequest.newPassword != resetPasswordRequest.comfirmPassword)
                {
                    return new Response<BaseResponse>(false, "Password doesn't match its confirmation", null);
                }
                user.UpdatedAt = DateTime.UtcNow;
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var Result = await _userManager.ResetPasswordAsync(user, token, resetPasswordRequest.newPassword);

                //await AddCodeNumber(user.Email);
                if (!Result.Succeeded)
                {
                    return new Response<BaseResponse>(false, "Something went wrong!", null);
                }
                user.CodeNumber = null;
                await _userManager.UpdateAsync(user);
                return new Response<BaseResponse>(true, "Password has been reset successfully", null);
            }
            catch (Exception ex)
            {
                return new Response<BaseResponse>(false, ex.Message, null);
            }
        }
        */
        //update user
        public async Task<BaseResponse> UpdateProfile(UserRequest userRequest)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userRequest.Id.ToString());
                if(_userRepository.checkRole(user.Id) != userRequest.Role)
                {
                    if(userRequest.Role == 3)
                    {
                        await _userManager.RemoveFromRoleAsync(user, UserRoles.Seller);
                        await _userManager.AddToRoleAsync(user, UserRoles.Customer);
                    }
                    else if(userRequest.Role == 2)
                    {
                        await _userManager.RemoveFromRoleAsync(user, UserRoles.Customer);
                        await _userManager.AddToRoleAsync(user, UserRoles.Seller);
                    }  
                }
                user.Fullname = userRequest.FullName;
                //user.UserName = userRequest.UserName;
                user.Gender = userRequest.Gender;
                user.Email = userRequest.Email;
                user.Address = userRequest.Address;
                user.Status = userRequest.Status;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new BaseResponse(false, "lỗi");
                }
                return new BaseResponse(true, "thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "Thất bại: "+ ex.Message);
                throw;
            }
        }
        //change status user
        public async Task<BaseResponse> ChangeStatus(Guid id)
        {
            try
            {
                var user =await _userManager.FindByIdAsync(id.ToString());
                if(user.Status == 2)
                {
                    user.Status = 1;
                }
                else
                {
                    user.Status = 2;
                }
                var result=await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new BaseResponse(false, "lỗi");
                }
                return new BaseResponse(true, user.Status.ToString());
            }
            catch ( Exception ex)
            {
                return new BaseResponse(false, "lỗi :" + ex.Message);
                throw;
            }
        }
        //delete Soft
        public async Task<BaseResponse> DeleteSoft(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user.Status == 0)
                {
                    user.Status = 2;
                }
                else
                {
                    user.Status = 0;
                }
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new BaseResponse(false, "lỗi");
                }
                return new BaseResponse(true, "Thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "lỗi :" + ex.Message);
                throw;
            }
        }
        //delete Hard
        public async Task<BaseResponse> Delete(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                user.IsDelete = true;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new BaseResponse(false, "lỗi");
                }
                return new BaseResponse(true, "Thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "lỗi :" + ex.Message);
                throw;
            }
        }

    }

}
