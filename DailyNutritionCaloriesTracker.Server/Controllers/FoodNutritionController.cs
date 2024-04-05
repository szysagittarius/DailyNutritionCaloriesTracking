using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DailyNutritionCaloriesTracker.Server.Controllers
{
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
        public IEnumerable<FoodNutrition> Get()
        {

            string json = "{ \"Egg\": { \"Carbohydrates (g/100g)\": 0.72, \"Fat (g/100g)\": 9.51, \"Proteins (g/100g)\": 12.56, \"Calories (kcal/100g)\": 143.48 } }";
            var foodNutritionDictionary = JsonConvert.DeserializeObject<Dictionary<string, FoodNutrition>>(json);

            List<FoodNutrition> foodNutritionList = foodNutritionDictionary.Select(kvp =>
            {
                FoodNutrition foodNutrition = kvp.Value;
                foodNutrition.Name = kvp.Key;
                return foodNutrition;
            }).ToList();

            return foodNutritionList;
        }


        private IEnumerable<FoodNutrition> LoadData()
        {
            //load data from json file, under App_data folder in the project
            var json = System.IO.File.ReadAllText("App_Data/food_nutritional_values.json");

            var foodNutritionDictionary = JsonConvert.DeserializeObject<Dictionary<string, FoodNutrition>>(json);

            List<FoodNutrition> foodNutritionList = foodNutritionDictionary.Select(kvp =>
            {
                FoodNutrition foodNutrition = kvp.Value;
                foodNutrition.Name = kvp.Key;
                foodNutrition.Measure = "per 100g";
                return foodNutrition;
            }).ToList();


            return foodNutritionList;
        }
    }
}
