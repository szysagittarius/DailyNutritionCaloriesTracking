namespace NT.Application.Contracts.Entities;
public class FoodLogEntity
{
    public double TotalCalories { get; set; }
    public double TotalCarbs { get; set; }
    public double TotalProtein { get; set; }
    public double TotalFat { get; set; }

    public Guid UserId { get; set; }

    public DateTime DateTime { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public IEnumerable<FoodItemEntity> FoodItems { get; set; } = new List<FoodItemEntity>(); // Assuming FoodItem is also refactored to a domain model



}
