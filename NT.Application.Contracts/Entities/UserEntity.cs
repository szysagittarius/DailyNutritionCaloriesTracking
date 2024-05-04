namespace NT.Application.Contracts.Entities;
public class UserEntity : EntityBase
{
    public new Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string password { get; set; }
}
