using AutoMapper;
using NT.Application.Contracts.Entities;

namespace NutritionTracker.Api.Profilers;

public class FoodNutritionDtoProfiler : Profile
{
    public FoodNutritionDtoProfiler()
    {
        // Mapping from Entity to DTO
        CreateMap<FoodNutritionEntity, FoodNutritionDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
            .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Name))
            .ForMember(dto => dto.Measurement, opt => opt.MapFrom(entity => entity.Measurement))
            .ForMember(dto => dto.Calories, opt => opt.MapFrom(entity => entity.Calories))
            .ForMember(dto => dto.Protein, opt => opt.MapFrom(entity => entity.Protein))
            .ForMember(dto => dto.Carbs, opt => opt.MapFrom(entity => entity.Carbs))
            .ForMember(dto => dto.Fat, opt => opt.MapFrom(entity => entity.Fat));

        // Reverse mapping from DTO to Entity
        CreateMap<FoodNutritionDto, FoodNutritionEntity>()
            .ForMember(entity => entity.Id, opt => opt.MapFrom(dto => dto.Id))
            .ForMember(entity => entity.Name, opt => opt.MapFrom(dto => dto.Name))
            .ForMember(entity => entity.Measurement, opt => opt.MapFrom(dto => dto.Measurement))
            .ForMember(entity => entity.Calories, opt => opt.MapFrom(dto => dto.Calories))
            .ForMember(entity => entity.Protein, opt => opt.MapFrom(dto => dto.Protein))
            .ForMember(entity => entity.Carbs, opt => opt.MapFrom(dto => dto.Carbs))
            .ForMember(entity => entity.Fat, opt => opt.MapFrom(dto => dto.Fat));
    }
}
