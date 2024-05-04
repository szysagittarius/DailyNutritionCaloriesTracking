using AutoMapper;
using NT.Application.Contracts.Entities;
using NT.Application.Contracts.Ports;
using NT.Database.Entities;
using NT.Ef.Repositories.Abstractions;

namespace NT.Ef.Repositories.Implementations.DataHandlers;
public class UserDataHandler : IUserDataHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserDataHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserEntity> GetUserAsync(string username)
    {
        User userModel = _userRepository.GetAll().Where(u => u.Name == username).FirstOrDefault();
        return _mapper.Map<UserEntity>(userModel);
    }

    public async Task<IEnumerable<UserEntity>> GetUsersAsync()
    {
        IEnumerable<User> users = await Task.FromResult(_userRepository.GetAll().ToList());
        return _mapper.Map<IEnumerable<UserEntity>>(users);
    }

    public async Task<UserEntity> AddUserAsync(UserEntity user)
    {
        try
        {
            User userModel = _mapper.Map<User>(user);
            User addedUser = await _userRepository.AddAsync(userModel);
            return _mapper.Map<UserEntity>(addedUser);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<UserEntity> UpdateUserAsync(UserEntity user)
    {
        User userModel = _mapper.Map<User>(user);
        User updatedUser = await _userRepository.UpdateAsync(userModel);
        return _mapper.Map<UserEntity>(updatedUser);
    }

    public async Task DeleteUserAsync(string username)
    {
        throw new System.NotImplementedException();
    }

    public async Task DeleteUserAsync(IEnumerable<string> usernames)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteUserAsync()
    {
        throw new System.NotImplementedException();
    }
}
