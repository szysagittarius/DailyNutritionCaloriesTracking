using NT.Application.Contracts.Entities;

namespace NT.Application.Contracts.Ports;

public interface IFoodNutritionDataHandler
{
    Task<FoodNutrition> GetFoodNutritionAsync(string foodName);
    Task<IEnumerable<FoodNutrition>> GetFoodNutritionAsync(IEnumerable<string> foodNames);
    Task<IEnumerable<FoodNutrition>> GetFoodNutritionAsync();
    Task<FoodNutrition> AddFoodNutritionAsync(FoodNutrition FoodNutrition);
    Task<FoodNutrition> UpdateFoodNutritionAsync(FoodNutrition FoodNutrition);
    Task DeleteFoodNutritionAsync(string foodName);
    Task DeleteFoodNutritionAsync(IEnumerable<string> foodNames);
    Task DeleteFoodNutritionAsync();
}
