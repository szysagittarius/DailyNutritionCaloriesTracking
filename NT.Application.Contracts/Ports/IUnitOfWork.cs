namespace NT.Application.Contracts.Ports;

public interface IUnitOfWork : IDisposable
{
    bool HasActiveTransaction();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
}

