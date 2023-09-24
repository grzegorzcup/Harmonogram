using Application.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
         IEnumerable<User> GetUsers();
         User GetById(int id);
         User GetByUserName(string userName);
         User AddUser(User user);
         void UpdateUser(User user);
         bool DeleteUser(int id);

    }
}
