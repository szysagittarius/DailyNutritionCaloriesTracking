using AutoMapper;
using NT.Application.Contracts.Entities;
using NutritionTracker.Api.Models;

namespace NutritionTracker.Api.Profilers;

public class UserDtoProfiler : Profile
{
    public UserDtoProfiler()
    {
        CreateMap<UserEntity, UserDto>().ReverseMap();
    }
}
