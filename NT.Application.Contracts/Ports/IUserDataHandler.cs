using NT.Application.Contracts.Entities;

namespace NT.Application.Contracts.Ports;
public interface IUserDataHandler
{
    Task<User> GetUserAsync(string username);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(string username);
    Task DeleteUserAsync(IEnumerable<string> usernames);
    Task DeleteUserAsync();
}
