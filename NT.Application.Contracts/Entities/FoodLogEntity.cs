namespace NT.Application.Contracts.Entities;
public class FoodLogEntity : EntityBase
{
    public new Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }

    public IEnumerable<FoodItemEntity> FoodItems { get; set; } // Assuming FoodItem is also refactored to a domain model


    public Guid UserId { get; set; }

    public FoodLogEntity(DateTime dateTime, Guid userId, IEnumerable<FoodItemEntity> foodItems)
    {

        if (userId == Guid.Empty)
        {
            throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        }

        Id = Guid.NewGuid();
        DateTime = dateTime;
        CreateTime = DateTime.Now;
        UpdateTime = DateTime.Now;
        UserId = userId;

        FoodItems = foodItems;
    }

    public FoodLogEntity()
    {
        CreateTime = DateTime.Now;
        UpdateTime = DateTime.Now;
        FoodItems = new List<FoodItemEntity>();
    }

    public void UpdateLog(DateTime dateTime)
    {
        DateTime = dateTime;
        UpdateTime = DateTime.Now;
    }

    // Additional methods to manipulate and query the food log
}
