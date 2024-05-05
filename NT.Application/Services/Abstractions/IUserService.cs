using NT.Application.Contracts.Entities;

namespace NT.Application.Services.Abstractions;
public interface IUserService
{
    Task<UserEntity> AddAsync(UserEntity user);
}
