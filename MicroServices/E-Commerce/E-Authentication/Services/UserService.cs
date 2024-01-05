using AutoMapper;
using E_Authentication.Data;
using E_Authentication.Models;
using E_Authentication.Models.Dtos;
using E_Authentication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Authentication.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwt _JwtServices;

        public UserService(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IJwt jwtServices)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _JwtServices = jwtServices;
        }

        public async Task<bool> AssignUserRoles(string Email, string RoleName)
        {
           
            var user = await _context.AppUsers.Where(x => x.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            else
            {
                if(!_roleManager.RoleExistsAsync(RoleName).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(RoleName));
                }
                await _userManager.AddToRoleAsync(user, RoleName);
                return true;
            }
        }

        public async Task<AppUser> GetUserById(string Id)
        {
            return await _context.AppUsers.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<LoginResponseDto> loginUser(LoginRequestDto loginRequestDto)
        {   //If Username Exists
            var user = await _context.AppUsers.Where(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower()).FirstOrDefaultAsync();
            //Comparing Password
            var isValid = _userManager.CheckPasswordAsync(user, loginRequestDto.Password).GetAwaiter().GetResult();
            if (!isValid || user == null)
            {
                return new LoginResponseDto();
            }
            var loggedUser = _mapper.Map<UserDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            var token = _JwtServices.GenerateToken(user, roles);

            var response = new LoginResponseDto()
            {
                User = loggedUser,
                Token = token
            };
            return response;

        }

        public async Task<string> RegisterUser(RegisterUserDto userDto)
        {
            try
            {
                var user = _mapper.Map<AppUser>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if(result.Succeeded)
                {
                    if(!_roleManager.RoleExistsAsync(userDto.Role).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(userDto.Role));
                    }
                    await _userManager.AddToRoleAsync(user, userDto.Role);
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
