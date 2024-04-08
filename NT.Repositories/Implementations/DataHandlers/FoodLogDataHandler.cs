using AutoMapper;
using NT.Application.Contracts.Entities;
using NT.Application.Contracts.Ports;
using NT.Database.Entities;
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

    public async Task<FoodLogEntity> AddAsync(FoodLogEntity foodLog)
    {
        FoodLog foodLogModel = _mapper.Map<FoodLog>(foodLog);
        FoodLog addedFoodLog = await _foodLogRepository.AddAsync(foodLogModel);
        return _mapper.Map<FoodLogEntity>(addedFoodLog);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _foodLogRepository.DeleteAsync(id); // Example conversion, adjust based on actual ID handling
    }

    public async Task<FoodLogEntity> GetAsync(Guid id)
    {
        FoodLog foodLog = await _foodLogRepository.GetByIdAsync(id); // Adjust ID conversion as necessary
        return _mapper.Map<FoodLogEntity>(foodLog);
    }

    public async Task<IEnumerable<FoodLogEntity>> GetAllAsync()
    {
        List<FoodLog> foodLogs = await Task.FromResult(_foodLogRepository.GetAll().ToList());
        return _mapper.Map<IEnumerable<FoodLogEntity>>(foodLogs);
    }

    public async Task<IEnumerable<FoodLogEntity>> GetAllAsync(Guid userId)
    {
        // Assuming there's a way to filter by userId in your repository
        List<FoodLog> foodLogs = _foodLogRepository.GetAll().Where(f => f.UserId == userId).ToList();
        return await Task.FromResult(_mapper.Map<IEnumerable<FoodLogEntity>>(foodLogs));
    }

    public async Task<FoodLogEntity> UpdateAsync(FoodLogEntity foodLog)
    {
        FoodLog foodLogModel = _mapper.Map<FoodLog>(foodLog);
        FoodLog updatedFoodLog = await _foodLogRepository.UpdateAsync(foodLogModel);
        return _mapper.Map<FoodLogEntity>(updatedFoodLog);
    }
}
