using NT.Application.Contracts.Entities;
using NT.Application.Contracts.Ports;
using NT.Application.Services.Abstractions;

namespace NT.Application.Services.Implementations;

internal class UserService : IUserService
{
    private readonly IUserDataHandler _userDataHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserDataHandler userDataHandler, IUnitOfWork unitOfWork)
    {
        _userDataHandler = userDataHandler;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserEntity> GetAsync(string username)
    {
        return await _userDataHandler.GetUserAsync(username);
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        return await _userDataHandler.GetUsersAsync();
    }

    public async Task<UserEntity> AddAsync(UserEntity user)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            UserEntity result = await _userDataHandler.AddUserAsync(user);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return result;
        }
        catch
        {
            if (_unitOfWork.HasActiveTransaction())
            {
                await _unitOfWork.RollbackAsync();
            }
            throw;
        }
    }

    public async Task<UserEntity> UpdateAsync(UserEntity user)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            UserEntity result = await _userDataHandler.UpdateUserAsync(user);
            await _unitOfWork.CommitAsync();
            return result;
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(string username)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _userDataHandler.DeleteUserAsync(username);
            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(IEnumerable<string> usernames)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            foreach (string username in usernames)
            {
                await _userDataHandler.DeleteUserAsync(username);
            }
            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public Task DeleteAsync()
    {
        throw new NotImplementedException();
    }
}
