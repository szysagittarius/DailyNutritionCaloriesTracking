using AutoMapper;
using NT.Application.Contracts.Entities;

namespace NutritionTracker.Api.Profilers;

public class FoodItemDtoProfiler : Profile
{
    public FoodItemDtoProfiler()
    {
        CreateMap<FoodItemEntity, FoodItemDto>().ReverseMap();
    }
}
