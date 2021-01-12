using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Core.Dtos;
using UserApi.Core.Models;

namespace UserApi.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<CretaeUserDto, User>();
            CreateMap<User, GetAllUserDto>();

        }
    }
}
