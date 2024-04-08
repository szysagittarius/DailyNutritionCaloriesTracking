using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DailyNutritionCaloriesTracker.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodNutritionController : ControllerBase
{

    private readonly ILogger<FoodNutritionController> _logger;

    public FoodNutritionController(ILogger<FoodNutritionController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetFoodNutrition")]
    public IEnumerable<FoodNutritionDto> Get()
    {
        return LoadData();
    }

    //if this changes to public, it will mess up the swagger documentation
    private IEnumerable<FoodNutritionDto> LoadData()
    {
        //load data from json file, under App_data folder in the project
        string json = System.IO.File.ReadAllText("App_Data/food_nutritional_values.json");

        Dictionary<string, FoodNutritionDto>? foodNutritionDictionary = JsonConvert.DeserializeObject<Dictionary<string, FoodNutritionDto>>(json);

        List<FoodNutritionDto> foodNutritionList = foodNutritionDictionary.Select(kvp =>
        {
            FoodNutritionDto foodNutrition = kvp.Value;
            foodNutrition.Name = kvp.Key;
            foodNutrition.Measure = "per 100g";
            return foodNutrition;
        }).ToList();


        return foodNutritionList;
    }
}
