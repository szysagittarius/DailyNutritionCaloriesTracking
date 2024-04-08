namespace NT.Application.Contracts.Entities;
public class FoodItemEntity : EntityBase
{
    public new Guid Id { get; private set; }
    public Guid FoodNutritionId { get; private set; }
    public FoodNutritionEntity FoodNutrition { get; private set; } // Assuming FoodNutrition is also refactored to a domain model
    public int Unit { get; private set; }
    public Guid FoodLogId { get; private set; }
    public FoodLogEntity FoodLog { get; private set; } // Assuming FoodLog is refactored as above

    public FoodItemEntity(Guid id, Guid foodNutritionId, FoodNutritionEntity foodNutrition, int unit, Guid foodLogId, FoodLogEntity foodLog)
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
