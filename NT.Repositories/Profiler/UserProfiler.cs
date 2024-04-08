using AutoMapper;
using NT.Application.Contracts.Entities;
using NT.Database.Entities;

namespace NT.Ef.Repositories.Profiler;
internal class UserProfiler : Profile
{
    public UserProfiler()
    {
        CreateMap<UserEntity, User>().ReverseMap();
    }
}
