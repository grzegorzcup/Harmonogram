using Application.DTO;
using Application.Interfaces;
using Application.Middleware.Expections;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository,IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public IEnumerable<User> GetUsers()
        {
            var list =_userRepository.GetUsers();
            return list;
        }
        public User GetUserById(int id)
        {
            if (id == 0)
                throw new Exception("nie ma użytkownika z id 0");
            if (id == null)
                throw new NullExpection("niewłaściwe id użytkownika");
            var user = _userRepository.GetById(id);
            return user;
        }

        public User GetUserByName(string name)
        {
            if (name == string.Empty)
                throw new Exception("nie ma użytkownika z id 0");
            if (name == null)
                throw new NullExpection("niewłaściwe id użytkownika");
            var user = _userRepository.GetByUserName(name);
            return user;
        }
        public User UpdateUser(UpdateUserDto user)
        {
            if (user == null)
                throw new NullExpection(" ");
            var update = _mapper.Map<User>(user);
            _userRepository.UpdateUser(update);
            update =  _userRepository.GetByUserName(user.UserName);
            return update;
        }
        public bool DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public User AddUser(User user)
        {
            if(user == null) 
                throw new NullExpection("nie można stworzyć użytkownika");
            var add = _userRepository.AddUser(user);
            return add;
        }
    }
}
