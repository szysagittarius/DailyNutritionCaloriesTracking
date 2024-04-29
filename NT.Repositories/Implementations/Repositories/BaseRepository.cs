using NT.Database.Context;
using NT.Ef.Repositories.Abstractions;

namespace NT.Ef.Repositories.Implementations.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly NutritionTrackerDbContext dbContext;

    public BaseRepository(NutritionTrackerDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        try
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    public async Task DeleteAsync(Guid id)
    {
        TEntity? entity = await dbContext.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            dbContext.Set<TEntity>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }

    public IQueryable<TEntity> GetAll()
    {
        return dbContext.Set<TEntity>().AsQueryable();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}

