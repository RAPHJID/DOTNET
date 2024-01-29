using AuctionMessageBus;
using AuthMicroService.Model.Dtos;
using AuthMicroService.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;



namespace AuthMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly ResponseDto _response;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(IUser user, IConfiguration configuration, IMapper mapper)
        {
            _response = new ResponseDto();
            _userService = user;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUserDto)
        {
            // default role of "Buyer" if not specified
            string role = string.IsNullOrEmpty(registerUserDto.Role) ? "Bidder" : registerUserDto.Role;

            // Call the service to register the user
            var res = await _userService.RegisterUser(registerUserDto, role);

            if (string.IsNullOrEmpty(res))
            {
                _response.Result = "User Registered Successfully!!";

                //add message to queue 

                var message = new UserMessageDto()
                {
                    Name = registerUserDto.Name,
                    Email = registerUserDto.Email,
                };

                var mb =new MessageBus();
                await mb.PublishMessage(message, _configuration.GetValue<string>("ServiceBus:register"));


                return Created("", _response);
            }

            _response.ErrorMessage = res;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto loginDto)
        {
            var res = await _userService.LoginUser(loginDto);
            if (res.User!=null)
            {
                _response.Result = res;
                return Created("", _response);
            }
            _response.ErrorMessage = "Invalid Credentials!!";
            _response.IsSuccess = false;
            return BadRequest(_response);
        }

        [Authorize("Admin")]
        [HttpPost("AssignRole")]
        public async Task<ActionResult<ResponseDto>> AssignRole(AssignRoleDto role)
        {
            var res = await _userService.AssignUserRoles(role.Email, role.Role);

            if (res)
            {
                // Success
                _response.Result = res;
                return Ok(_response);
            }

            _response.ErrorMessage = "Error Occurred"; 
            _response.Result = res;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }

    }
}
