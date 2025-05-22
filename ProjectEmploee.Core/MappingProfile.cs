using AutoMapper;
using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Attendance, AttendanceDTO>().ReverseMap();
            CreateMap<Request, RequestDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserPostDTO, User>();

        }
    }
}
