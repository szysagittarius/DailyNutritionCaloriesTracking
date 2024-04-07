using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT.Database.Entities;

[Table("FoodNutritions")]
public record class FoodNutrition
{
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required Guid Id { get; set; }

    [Column("Name")]
    public required string Name { get; set; }

    [Column("Measurement")]
    public required string Measurement { get; set; }

    [Column("Carbs")]
    public required double Carbs { get; set; }
    
    [Column("Fat")]
    public required double Fat { get; set; }

    [Column("Protein")]
    public required double Protein { get; set; }

    [Column("Calories")]
    public required double Calories { get; set; }


}
