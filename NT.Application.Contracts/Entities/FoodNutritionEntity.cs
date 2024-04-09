namespace NT.Application.Contracts.Entities;

public class FoodNutritionEntity
{
    //public Guid Id { get; set; }
    public string Name { get; set; }
    public string Measurement { get; set; }
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Carbs { get; set; }
    public double Fat { get; set; }
}
