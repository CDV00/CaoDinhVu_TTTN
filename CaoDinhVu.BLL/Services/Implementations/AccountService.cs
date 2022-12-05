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

        public AccountService(UserManager<AppUser> userManager, IMapper mapper, IUserRepository userRepository, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
            _roleManager = roleManager;
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
                userResponse.Role = string.Join(",", roles);

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
                if (registerRequest.Role == "Saler")
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

    }

}
