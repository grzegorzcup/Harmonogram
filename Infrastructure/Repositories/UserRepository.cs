using Application.DTO;
using Application.Middleware.Expections;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }
        public User AddUser(User user)
        {
            _context.Add(user);
            return user;
        }

        public bool DeleteUser(int id)
        {
            var user = GetById(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public User GetById(int id)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(x => x.Id == id);
            return user;
        }

        public User GetByUserName(string userName)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(x => x.Username == userName);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.Include(u=> u.Role);
        }

        public void UpdateUser(User user)
        {
            var updateUser = GetByUserName(user.Username);
            using (var context = _context)
            {
                context.Users.Attach(updateUser);
                context.Entry(updateUser).Property(p => p.Role).IsModified = true;
                context.Entry(updateUser).Property(p => p.RoleId).IsModified = true;
                context.SaveChanges();
            }
        }
    }
}
