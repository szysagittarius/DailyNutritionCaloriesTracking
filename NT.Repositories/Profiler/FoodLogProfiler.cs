using AutoMapper;
using NT.Application.Contracts.Entities;
using NT.Database.Entities;

namespace NT.Ef.Repositories.Profiler;
internal class FoodLogProfiler : Profile
{
    public FoodLogProfiler()
    {
        CreateMap<FoodLogEntity, FoodLog>().ReverseMap();
    }
}
