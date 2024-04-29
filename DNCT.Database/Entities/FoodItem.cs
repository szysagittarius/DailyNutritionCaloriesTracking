using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NT.Database.Entities;

[Table("FoodItems")]
public record FoodItem
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier")]
    public required Guid Id { get; init; }

    [ForeignKey("FoodNutrition")]
    [Column("FoodNutritionId", TypeName = "uniqueidentifier")]
    public required Guid FoodNutritionId { get; init; }

    public required FoodNutrition FoodNutrition { get; init; }

    [Column("Unit", TypeName = "int")]
    public required int Unit { get; init; }

    [ForeignKey("FoodLog")]
    [Column("FoodLogId", TypeName = "uniqueidentifier")]
    public required Guid FoodLogId { get; init; }

    public required FoodLog FoodLog { get; init; }
}
