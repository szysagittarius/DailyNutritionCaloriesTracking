using NT.Application.Contracts.Entities;

namespace NT.Application.Contracts.Ports;
public interface IUserDataHandler
{
    Task<UserEntity> GetAsync(string username);
    Task<IEnumerable<UserEntity>> GetAllAsync();
    Task<UserEntity> AddAsync(UserEntity user);
    Task<UserEntity> UpdateAsync(UserEntity user);
    Task DeleteAsync(string username);
    Task DeleteAsync(IEnumerable<string> usernames);
    Task DeleteAsync();
}
