using AutoMapper;
using NT.Application.Contracts.Entities;

namespace NutritionTracker.Api.Profilers;

public class UserDtoProfiler : Profile
{
    public UserDtoProfiler()
    {
        CreateMap<UserEntity, UserDto>().ReverseMap();
    }
}
