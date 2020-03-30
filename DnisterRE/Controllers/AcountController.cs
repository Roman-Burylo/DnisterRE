using AutoMapper;
using Bll.DTO;
using Bll.Interfaces;
using Bll.Services;
using DnisterRE.DTO.UsersDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace DnisterRE.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserManagmentService _userManagement;
        private readonly IConfirmationService _confirmation;

        public AcountController(IAuthenticationService authService, IUserManagmentService userManagement, IConfirmationService confirmation)
        {
            _authService = authService;
            _userManagement = userManagement;
            _confirmation = confirmation;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] LoginModel login)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<LoginModel, LoginViewModel>()).CreateMapper();
            var loginDto = mapper.Map<LoginModel, LoginModel>(login);
            var user = await _userManagement.GetUser(loginDto.Email, loginDto.Password);
            var token = await _authService.Authenticate(user);
            if (token == "" || token == null)
            {
                return BadRequest("Invalid Request");
            }

            return Ok(new LoginResponseDto()
            {
                Token = token,
            });
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromForm] UserAllViewModel newUser)
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserAllViewModel, UserAllDto>();
                cfg.CreateMap<UserAllViewModel, ConfirmUserDto>();
            }).CreateMapper();

            var user = mapper.Map<UserAllViewModel, UserAllDto>(newUser);
            var confirmUser = mapper.Map<UserAllViewModel, ConfirmUserDto>(newUser);
            await _userManagement.AddUser(user);
            return Ok();
        }

        [HttpPost("confirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmUserViewModel confirmUser)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ConfirmUserViewModel, ConfirmUserDto>()).CreateMapper();
            var confirm = mapper.Map<ConfirmUserViewModel, ConfirmUserDto>(confirmUser);
            await _confirmation.ConfirmUser(confirm);
            return Ok();
        }

        [HttpPost("password/forgot")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody]ConfirmPasswordChangingViewModel req)
        {
            await _userManagement.PasswordConfirmationRequest(req.Email);
            return Ok();
        }

        [HttpPost("password/reset")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmUser([FromBody]ConfirmPasswordChangingViewModel confirmPassword)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ConfirmPasswordChangingViewModel, ConfirmPasswordChangingDto>()).CreateMapper();
            var confirm = mapper.Map<ConfirmPasswordChangingViewModel, ConfirmPasswordChangingDto>(confirmPassword);
            await _confirmation.ConfirmPassword(confirm);
            return Ok();
        }
    }
}