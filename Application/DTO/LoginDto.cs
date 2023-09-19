using Application.Middleware.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class LoginDto: IMap
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginDto, User>();
        }
    }
}
