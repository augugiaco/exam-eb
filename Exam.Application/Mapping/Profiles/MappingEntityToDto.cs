using AutoMapper;
using Exam.Application.Dtos;
using Exam.Common.Extensions;
using Exam.Domain.Entities;

namespace Exam.Application.Mapping.Profiles
{
    public class MappingEntityToDto:Profile
    {

        public MappingEntityToDto()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<PagedResultDto<UserDto>, PagedResult<User>>().ReverseMap();
        }

        
    }
}
