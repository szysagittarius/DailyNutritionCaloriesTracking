using NT.Application.Contracts.Entities;
using NT.Application.Contracts.Ports;
using NT.Application.Services.Abstractions;

namespace NT.Application.Services.Implementations;
public class UserService : IUserService
{
    private readonly IUserDataHandler _userDataHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserDataHandler userDataHandler, IUnitOfWork unitOfWork)
    {
        _userDataHandler = userDataHandler;
        _unitOfWork = unitOfWork;
    }
    public async Task<UserEntity> AddAsync(UserEntity userEntity)
    {
        await _unitOfWork.BeginTransactionAsync();  // Start the transaction
        try
        {
            UserEntity result = await _userDataHandler.AddAsync(userEntity);
            await _unitOfWork.CommitAsync();  // Commit the transaction
            return result;
        }
        catch
        {
            await _unitOfWork.RollbackAsync();  // Rollback on error
            throw;
        }
    }
}
