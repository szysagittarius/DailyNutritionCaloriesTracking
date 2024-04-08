
using NT.Database.Context;
using NT.Database.Entities;
using NT.Ef.Repositories.Abstractions;

namespace NT.Ef.Repositories.Implementations.Repositories;
internal class FoodLogRepository : BaseRepository<FoodLog>, IFoodLogRepository
{
    public FoodLogRepository(NutritionTrackerDbContext dbContext) : base(dbContext)
    {
    }
}
