using NT.Application.Contracts.Entities;
using NT.Application.Contracts.Ports;
using NT.Application.Services.Abstractions;

namespace NT.Application.Services.Implementations;

internal class FoodLogService : IFoodLogService
{
    private readonly IFoodLogDataHandler _foodLogDataHandler;
    private readonly IUnitOfWork _unitOfWork;

    public FoodLogService(IFoodLogDataHandler foodLogDataHandler, IUnitOfWork unitOfWork)
    {
        _foodLogDataHandler = foodLogDataHandler;
        _unitOfWork = unitOfWork;
    }

    public async Task<FoodLogEntity> AddAsync(FoodLogEntity foodLog)
    {
        FoodLogEntity result = await _foodLogDataHandler.AddAsync(foodLog);
        await _unitOfWork.CommitAsync();
        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _foodLogDataHandler.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(IEnumerable<Guid> ids)
    {
        foreach (Guid id in ids)
        {
            await _foodLogDataHandler.DeleteAsync(id);
        }
        await _unitOfWork.CommitAsync();
    }

    public Task DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<FoodLogEntity> GetAsync(Guid id)
    {
        return await _foodLogDataHandler.GetAsync(id);
    }

    public async Task<IEnumerable<FoodLogEntity>> GetAllAsync()
    {
        return await _foodLogDataHandler.GetAllAsync();
    }

    public async Task<IEnumerable<FoodLogEntity>> GetAllAsync(Guid userId)
    {
        return await _foodLogDataHandler.GetAllAsync(userId);
    }

    public async Task<FoodLogEntity> UpdateAsync(FoodLogEntity foodLog)
    {
        FoodLogEntity result = await _foodLogDataHandler.UpdateAsync(foodLog);
        await _unitOfWork.CommitAsync();
        return result;
    }
}
