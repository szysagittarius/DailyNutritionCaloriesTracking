using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NT.Database.Entities;

[Table("FoodLogs")]
public record FoodLog
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier")]
    public required Guid Id { get; init; }


    [Column("DateTime", TypeName = "datetime")]
    public required DateTime DateTime { get; init; }

    [Column("CreateTime", TypeName = "datetime")]
    public required DateTime CreateTime { get; init; }

    [Column("UpdateTime", TypeName = "datetime")]
    public required DateTime UpdateTime { get; init; }

    [ForeignKey("User")]
    [Column("UserId", TypeName = "uniqueidentifier")]
    public required Guid UserId { get; init; }

    public required User User { get; init; }


    public IEnumerable<FoodItem> FoodItems { get; init; } = Enumerable.Empty<FoodItem>();

}
