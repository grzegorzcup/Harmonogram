using Application.DTO;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public AccountController(ILoginService loginService,IUserService userService)
        {
           _loginService = loginService;
           _userService = userService;
        }
        [HttpGet("Register")]
        public IActionResult Register(RegisterUserDto user) 
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(user == null)
                return NotFound();
            var newUser = _loginService.Register(user);
            return Ok(newUser);
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (loginDto == null)
                return NotFound();
            var token = _loginService.Login(loginDto);
            return Ok(token);

        }
    }
}
