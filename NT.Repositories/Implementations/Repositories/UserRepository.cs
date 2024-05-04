using NT.Database.Context;
using NT.Database.Entities;
using NT.Ef.Repositories.Abstractions;

namespace NT.Ef.Repositories.Implementations.Repositories;
public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(NutritionTrackerDbContext dbContext) : base(dbContext)
    {
    }
}
