using AutoMapper;
using E_Authentication.Models.Dtos;
using E_Authentication.Services;
using E_Authentication.Services.IServices;
using E_MessageBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userservice;
        private readonly ResponseDto _response;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(IUser user,IConfiguration configuration, IMapper mapper )
        {
            _userservice = user;
            _configuration = configuration;
            _mapper = mapper;
            _response = new ResponseDto();
        }
       
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUserDto)
        {
            var res = await _userservice.RegisterUser(registerUserDto);

            if (string.IsNullOrWhiteSpace(res))
            {
                _response.Result = "User Rgistered Successfully";

                //queue
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

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> loginUser(LoginRequestDto loginRequestDto)
        {
            var res = await _userservice.loginUser(loginRequestDto);
            if (res.User!=null)
            {
                _response.Result= res;
                return Created("", _response);
            }
            _response.ErrorMessage = "Invalid Credentials";
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        [HttpPost("AssignRole")]
        public async Task<ActionResult<ResponseDto>> AssignRole(AssignRoleDto assignRole) 
        {
            var res = await _userservice.AssignUserRoles(assignRole.Email, assignRole.Role);
            if (res)
            {
                _response.Result = res;
                return Ok(_response);
            }
            _response.ErrorMessage = "Something went wrong!!";
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetUser(string Id)
        {
            var res = await _userservice.GetUserById(Id);
            var user = _mapper.Map<UserDto>(res);
            if(res != null)
            {
                _response.Result = user;
                return Ok(_response);
            }
            _response.ErrorMessage = "Invalid User!!";
            _response.IsSuccess = false;
            return NotFound(_response);
        }

    }
    
}
