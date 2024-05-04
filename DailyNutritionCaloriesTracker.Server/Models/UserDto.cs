namespace NutritionTracker.Api.Models;

public class UserDto : DtoBase
{
    public new Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string password { get; set; }

}
