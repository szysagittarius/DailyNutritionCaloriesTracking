using AutoMapper;
using NT.Application.Contracts.Entities;
using NT.Application.Contracts.Ports;
using NT.Database.Entities;
using NT.Ef.Repositories.Abstractions;

namespace NT.Ef.Repositories.Implementations.DataHandlers;
internal class UserDataHandler : IUserDataHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserDataHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserEntity> AddAsync(UserEntity userEntity)
    {
        User user = _mapper.Map<User>(userEntity);
        User addedUser = await _userRepository.AddAsync(user);
        return _mapper.Map<UserEntity>(addedUser);
    }


}
