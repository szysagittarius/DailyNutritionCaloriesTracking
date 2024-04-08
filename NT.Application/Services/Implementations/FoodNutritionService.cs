using NT.Application.Contracts.Entities;
using NT.Application.Contracts.Ports;
using NT.Application.Services.Abstractions;

namespace NT.Application.Services.Implementations;
internal class FoodNutritionService : IFoodNutritionService
{
    private readonly IFoodNutritionDataHandler _foodNutritionDataHandler;
    private readonly IUnitOfWork _unitOfWork;

    public FoodNutritionService(IFoodNutritionDataHandler foodNutritionDataHandler,
        IUnitOfWork unitOfWork)
    {
        _foodNutritionDataHandler = foodNutritionDataHandler;
        _unitOfWork = unitOfWork;
    }
    public async Task<FoodNutritionEntity> AddFoodNutritionAsync(FoodNutritionEntity foodNutrition)
    {
        FoodNutritionEntity result = await _foodNutritionDataHandler.AddAsync(foodNutrition);
        await _unitOfWork.CommitAsync();
        return result;
    }

    public async Task DeleteFoodNutritionAsync(string foodName)
    {
        await _foodNutritionDataHandler.DeleteAsync(foodName);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteFoodNutritionAsync(IEnumerable<string> foodNames)
    {
        foreach (string foodName in foodNames)
        {
            await _foodNutritionDataHandler.DeleteAsync(foodName);
        }
        await _unitOfWork.CommitAsync();
    }

    public Task DeleteFoodNutritionAsync()
    {
        throw new NotImplementedException();
    }

    //public async Task<FoodNutritionEntity> GetFoodNutritionAsync(string foodName)
    //{
    //    return await _foodNutritionDataHandler.GetByNameAsync(foodName);
    //}

    //public async Task<IEnumerable<FoodNutritionEntity>> GetFoodNutritionAsync(IEnumerable<string> foodNames)
    //{
    //    List<FoodNutritionEntity> result = new List<FoodNutritionEntity>();
    //    foreach (string foodName in foodNames)
    //    {
    //        var foodNutrition = await _foodNutritionDataHandler.GetByNameAsync(foodName);
    //        if (foodNutrition != null)
    //        {
    //            result.Add(foodNutrition);
    //        }
    //    }
    //    return result;
    //}

    public async Task<IEnumerable<FoodNutritionEntity>> GetFoodNutritionAsync()
    {
        return await _foodNutritionDataHandler.GetAllAsync();
    }

    public Task<IEnumerable<FoodNutritionEntity>> GetFoodNutritionAsync(IEnumerable<string> foodNames)
    {
        throw new NotImplementedException();
    }

    public async Task<FoodNutritionEntity> UpdateFoodNutritionAsync(FoodNutritionEntity foodNutrition)
    {
        FoodNutritionEntity result = await _foodNutritionDataHandler.UpdateAsync(foodNutrition);
        await _unitOfWork.CommitAsync();
        return result;
    }
}
