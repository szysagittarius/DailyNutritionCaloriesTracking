using Microsoft.EntityFrameworkCore.Storage;
using NT.Application.Contracts.Ports;
using NT.Database.Context;

namespace NT.Ef.Repositories.Implementations;
public class UnitOfWork : IUnitOfWork
{
    private readonly NutritionTrackerDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(NutritionTrackerDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public bool HasActiveTransaction()
    {
        return _transaction != null;
    }

    public async Task CommitAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("Transaction has not been started");
        }

        await _transaction?.CommitAsync();

    }

    public async Task RollbackAsync()
    {
        await _transaction?.RollbackAsync();
        //await DisposeTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }


    public void Dispose()
    {
        //_transaction?.Dispose();
        //_context?.Dispose();

        // Do not dispose the _context here if it's being managed by DI
        // Dispose only the transaction
        //DisposeTransactionAsync().Wait();
        GC.SuppressFinalize(this);
    }


}