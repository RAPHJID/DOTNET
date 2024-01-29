using AuthMicroService.Data;
using AuthMicroService.Model;
using AuthMicroService.Model.Dtos;
using AuthMicroService.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroService.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwt _JwtService;
        

        public UserService(AppDbContext appDbContext, IMapper mapper, UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager, IJwt jwtService) 
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _JwtService = jwtService; 
        }

        /*public async Task<bool> AssignUserRoles(string Email, string RoleName)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if(user == null)
            {
                //if user doesn't exist
                return false;
            }
            //Check if Seller role exist and create it if it doesn't
            if(!await _roleManager.RoleExistsAsync("Seller"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Seller"));
            }
            //Check if Bidder role exist and create it if it doesn't
            if (!await _roleManager.RoleExistsAsync("Bidder"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Bidder"));
            }
            

            //AssignUserRoles the user roles
            var result = await _userManager.AddToRoleAsync(user, RoleName);
            return result.Succeeded;
        }*/
        public async Task<bool> AssignUserRoles(string Email, string RoleName)
        {
            var user = await _appDbContext.ApplicationUsers.Where(x => x.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();
            if(user == null)
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


        public async Task<LoginResponseDto> LoginUser(LoginRequestDto loginDto)
        {
            var user = await _appDbContext.ApplicationUsers
                .Where(x => x.UserName.ToLower() == loginDto.UserName.ToLower())
                .FirstOrDefaultAsync();

            if (user == null)
            {
                // User not found
                return new LoginResponseDto { ErrorMessage = "User not found" };
            }

            /*if (loginDto.UserName.ToLower() == "admin@gmail.com" && loginDto.Password == "adminpassword")
            {
                // Admin login
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Assign the admin role
                await _userManager.AddToRoleAsync(user, "Admin");

                var adminDto = _mapper.Map<UserDto>(user);

                var res = new LoginResponseDto
                {
                    User = adminDto,
                    Token = "Admin token here..."
                };

                return res;
            }*/
            else
            {
                // Regular user login 
                var isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (!isValid)
                {
                    // Invalid password
                    return new LoginResponseDto { ErrorMessage = "Invalid password" };
                }

                var loggedUserDto = _mapper.Map<UserDto>(user);
                var roles = await _userManager.GetRolesAsync(user);
                var token = _JwtService.GenerateToken(user, roles);

                var res = new LoginResponseDto
                {
                    User = loggedUserDto,
                    Token = token
                };

                return res;
            }
        }



        public async Task<string> RegisterUser(RegisterUserDto userDto, string role)
        {
            // Check if the role exists, and create it if it doesn't
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            try
            {
                var user = _mapper.Map<AppUser>(userDto);

                // Create user
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    // Assign the role
                    await _userManager.AddToRoleAsync(user, role);

                    return ""; // successful
                }
                else
                {
                    return result.Errors.FirstOrDefault()?.Description ?? "User creation failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}






