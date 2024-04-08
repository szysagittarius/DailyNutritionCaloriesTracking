using Newtonsoft.Json;

namespace DailyNutritionCaloriesTracker.Server;

public class FoodNutritionDto
{
    public required string Name { get; set; }
    public required string Measure { get; set; }

    [JsonProperty("Calories (kcal/100g)")]
    public double Calories { get; set; }

    [JsonProperty("Proteins (g/100g)")]
    public double Protein { get; set; }

    [JsonProperty("Carbohydrates (g/100g)")]
    public double Carbs { get; set; }

    [JsonProperty("Fat (g/100g)")]
    public double Fat { get; set; }
}
