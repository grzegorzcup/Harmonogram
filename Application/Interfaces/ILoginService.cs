using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateJWT(User user);
        User Register(RegisterUserDto registerUser);
        string Login(LoginDto loginDto);
    }
}
