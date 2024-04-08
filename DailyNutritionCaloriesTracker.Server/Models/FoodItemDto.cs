using NutritionTracker.Api.Models;
public class FoodItemDto : DtoBase
{
    public new Guid Id { get; private set; }
    public Guid FoodNutritionId { get; private set; }
    public FoodNutritionDto FoodNutrition { get; private set; } // Assuming FoodNutrition is also refactored to a domain model
    public int Unit { get; private set; }
    public Guid FoodLogId { get; private set; }
    public FoodLogDto FoodLog { get; private set; } // Assuming FoodLog is refactored as above

    public FoodItemDto(Guid id, Guid foodNutritionId, FoodNutritionDto foodNutrition, int unit, Guid foodLogId, FoodLogDto foodLog)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty.", nameof(id));
        }

        if (foodNutritionId == Guid.Empty)
        {
            throw new ArgumentException("FoodNutritionId cannot be empty.", nameof(foodNutritionId));
        }

        if (foodLogId == Guid.Empty)
        {
            throw new ArgumentException("FoodLogId cannot be empty.", nameof(foodLogId));
        }

        if (unit <= 0)
        {
            throw new ArgumentException("Unit must be greater than zero.", nameof(unit));
        }

        Id = id;
        FoodNutritionId = foodNutritionId;
        FoodNutrition = foodNutrition ?? throw new ArgumentNullException(nameof(foodNutrition), "FoodNutrition cannot be null.");
        Unit = unit;
        FoodLogId = foodLogId;
        FoodLog = foodLog ?? throw new ArgumentNullException(nameof(foodLog), "FoodLog cannot be null.");
    }

    // Additional methods to manipulate and query the food item
}
