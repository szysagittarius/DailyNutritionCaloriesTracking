namespace NT.Application.Contracts.Entities;
public class FoodLogEntity : EntityBase
{
    public new Guid Id { get; private set; }
    public DateTime DateTime { get; private set; }
    public DateTime CreateTime { get; private set; }
    public DateTime UpdateTime { get; private set; }
    public Guid UserId { get; private set; }
    public UserEntity User { get; private set; } // Assuming User is also refactored to a domain model

    public FoodLogEntity(Guid id, DateTime dateTime, Guid userId, UserEntity user)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty.", nameof(id));
        }

        if (userId == Guid.Empty)
        {
            throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        }

        Id = id;
        DateTime = dateTime;
        CreateTime = DateTime.Now;
        UpdateTime = DateTime.Now;
        UserId = userId;
        User = user ?? throw new ArgumentNullException(nameof(user), "User cannot be null.");
    }

    public void UpdateLog(DateTime dateTime)
    {
        DateTime = dateTime;
        UpdateTime = DateTime.Now;
    }

    // Additional methods to manipulate and query the food log
}
