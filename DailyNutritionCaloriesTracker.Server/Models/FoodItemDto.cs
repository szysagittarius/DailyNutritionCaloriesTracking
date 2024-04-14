using NutritionTracker.Api.Models;
public class FoodItemDto : DtoBase
{
    public Guid FoodNutritionId { get; private set; }
    public int Unit { get; private set; }
    public Guid FoodLogId { get; private set; }


}
