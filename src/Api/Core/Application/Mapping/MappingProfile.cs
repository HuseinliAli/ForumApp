using AutoMapper;
using Common.Models.Queries;
using Common.Models.RequestModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();

            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
