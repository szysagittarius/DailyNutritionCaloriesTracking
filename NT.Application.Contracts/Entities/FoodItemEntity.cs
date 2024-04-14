namespace NT.Application.Contracts.Entities;
public class FoodItemEntity : EntityBase
{
    public Guid FoodNutritionId { get; private set; }
    public int Unit { get; private set; }
    public Guid FoodLogId { get; private set; }



    // Additional methods to manipulate and query the food item
}
