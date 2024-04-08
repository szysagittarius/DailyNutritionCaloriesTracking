using NT.Application.Contracts.Entities;

namespace NT.Application.Contracts.Ports;
public interface IFoodLogDataHandler
{
    Task<FoodLog> GetFoodLogAsync(Guid id);
    Task<IEnumerable<FoodLog>> GetFoodLogsAsync();
    Task<IEnumerable<FoodLog>> GetFoodLogsAsync(Guid userId);
    Task<FoodLog> AddFoodLogAsync(FoodLog foodLog);
    Task<FoodLog> UpdateFoodLogAsync(FoodLog foodLog);
    Task DeleteFoodLogAsync(Guid id);
}
