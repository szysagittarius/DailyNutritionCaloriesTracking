using AutoMapper;
using NT.Application.Contracts.Entities;

namespace NutritionTracker.Api.Profilers;

public class FoodLogDtoProfiler : Profile
{
    public FoodLogDtoProfiler()
    {
        CreateMap<FoodLogEntity, FoodLogDto>().ReverseMap();
    }
}
