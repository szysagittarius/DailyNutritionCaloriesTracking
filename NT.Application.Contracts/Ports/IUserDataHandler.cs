using NT.Application.Contracts.Entities;

namespace NT.Application.Contracts.Ports;
public interface IUserDataHandler
{
    Task<UserEntity> GetUserAsync(string username);
    Task<IEnumerable<UserEntity>> GetUsersAsync();
    Task<UserEntity> AddUserAsync(UserEntity user);
    Task<UserEntity> UpdateUserAsync(UserEntity user);
    Task DeleteUserAsync(string username);
    Task DeleteUserAsync(IEnumerable<string> usernames);
    Task DeleteUserAsync();
}
