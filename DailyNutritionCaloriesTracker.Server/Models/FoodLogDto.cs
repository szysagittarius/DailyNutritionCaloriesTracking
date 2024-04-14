using NutritionTracker.Api.Models;
public class FoodLogDto : DtoBase
{
    public DateTime DateTime { get; set; }

    public double TotalCalories { get; set; }
    public double TotalCarbs { get; set; }
    public double TotalProtein { get; set; }
    public double TotalFat { get; set; }

    IEnumerable<FoodItemDto> FoodItems { get; set; } // Assuming FoodItem is also refactored to a domain model

    public DateTime CreateTime { get; private set; }
    public DateTime UpdateTime { get; private set; }
    public Guid UserId { get; private set; }

    public FoodLogDto(Guid id, DateTime dateTime, Guid userId, UserDto user)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty.", nameof(id));
        }

        if (userId == Guid.Empty)
        {
            throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        }

        DateTime = dateTime;
        CreateTime = DateTime.Now;
        UpdateTime = DateTime.Now;
        UserId = userId;
    }

    public FoodLogDto()
    {
    }

    public void UpdateLog(DateTime dateTime)
    {
        DateTime = dateTime;
        UpdateTime = DateTime.Now;
    }

    // Additional methods to manipulate and query the food log
}
