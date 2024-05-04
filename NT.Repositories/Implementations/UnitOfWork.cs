using Microsoft.EntityFrameworkCore.Storage;
using NT.Application.Contracts.Ports;
using NT.Database.Context;

namespace NT.Ef.Repositories.Implementations;
public class UnitOfWork(NutritionTrackerDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public async Task BeginTransactionAsync()
    {
        _transaction = await context.Database.BeginTransactionAsync();
    }

    public bool HasActiveTransaction()
    {
        return _transaction != null;
    }

    public async Task CommitAsync()
    {
        try
        {
            await _transaction?.CommitAsync();
        }
        catch
        {
            // Log exception, handle error as needed
            await RollbackAsync();
            throw;
        }
        finally
        {
            //await _transaction?.DisposeAsync();
            //_transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        await _transaction?.RollbackAsync();
        //await DisposeTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        //_transaction?.Dispose();
        //_context?.Dispose();

        // Do not dispose the _context here if it's being managed by DI
        // Dispose only the transaction
        //DisposeTransactionAsync().Wait();
    }


}