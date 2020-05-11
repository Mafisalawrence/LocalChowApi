using AutoMapper;
using LocalChow.Domain.DTO;
using LocalChow.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalChow.Api.Extentions
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<StoreDto, Store>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<RoleDto, Role>().ReverseMap();
        }
    }
}
