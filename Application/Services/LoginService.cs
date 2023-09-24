using Application.Authentication;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Middleware.Expections;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _settings;
        private readonly ILogger<LoginService> _logger;

        public LoginService(IUserService userService,
                            IMapper mapper,
                            IPasswordHasher<User> passwordHasher,
                            AuthenticationSettings settings,
                            ILogger<LoginService> logger)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _settings = settings;
        }
        public string GenerateJWT(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name.ToString()),
                new Claim(ClaimTypes.Name, user.Username.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(double.Parse(_settings.JWTExpiredDays));

            var token = new JwtSecurityToken(_settings.JWTIssuer,
                                             _settings.JWTIssuer,
                                             claims, expires: expires,
                                             signingCredentials: cred);
            var tokenhandler = new JwtSecurityTokenHandler();
            return tokenhandler.WriteToken(token);

        }

        public string Login(LoginDto loginDto)
        {
            var user = _userService.GetUserByName(loginDto.UserName);
            if (user == null)
                throw new NullExpection("nie ma takeigo użytkownika");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("nieprawidłowe hasło");
            var token = GenerateJWT(user);
            return token;
        }

        public User Register(RegisterUserDto registerUser)
        {
            if (registerUser == null) throw new NullExpection("błędne dane rejestrowanego użytkownika");

            if (registerUser.RoleId == 0)
                throw new Exception("brak ID roli uzytkownika");
            var user = _mapper.Map<User>(registerUser);
            var hashedpassword = _passwordHasher.HashPassword(user,registerUser.Password);
            user.PasswordHash = hashedpassword;
            _userService.AddUser(user);
            return user;
        }
    }
}
