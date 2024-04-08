using AutoMapper;
using NT.Application.Contracts.Entities;
using NT.Application.Contracts.Ports;
using NT.Ef.Repositories.Abstractions;

namespace NT.Ef.Repositories.Implementations.DataHandlers;
public class FoodLogDataHandler : IFoodLogDataHandler
{
    private readonly IFoodLogRepository _foodLogRepository;
    private readonly IMapper _mapper;

    public FoodLogDataHandler(IFoodLogRepository foodLogRepository, IMapper mapper)
    {
        _foodLogRepository = foodLogRepository;
        _mapper = mapper;
    }

    public Task<FoodLog> AddFoodLogAsync(FoodLog foodLog)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFoodLogAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<FoodLog> GetFoodLogAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FoodLog>> GetFoodLogsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FoodLog>> GetFoodLogsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<FoodLog> UpdateFoodLogAsync(FoodLog foodLog)
    {
        throw new NotImplementedException();
    }
}
