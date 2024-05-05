using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NT.Application.Contracts.Entities;
using NT.Application.Services.Abstractions;
using NutritionTracker.Api.Models;
using NutritionTracker.Api.Profilers;

namespace DailyNutritionCaloriesTracker.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(ILogger<UserController> logger,
        IUserService userService,
        IMapper mapper)
    {
        _logger = logger;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("getusers")]
    public async Task<IEnumerable<UserDto>> GetAsync()
    {
        MapperConfiguration dtoMapperConfig = new MapperConfiguration(cfg =>
        {

            cfg.AddProfile(new UserDtoProfiler());
        });

        IMapper dtoMapper = dtoMapperConfig.CreateMapper();

        //load FoodLogDto from database by calling the service
        IEnumerable<UserEntity> entities = await _userService.GetAllAsync();

        IEnumerable<UserDto> userDtos = entities.Select(e => dtoMapper.Map<UserDto>(e));

        return userDtos;
    }

    [HttpPost("createuser")]
    public async Task<IActionResult> PostAsync([FromBody] UserDto userDto)
    {
        //insert the UserDto to the database by calling the service

        UserEntity entity3 = new UserEntity { Name = "test", Email = "test", Password = "test" };
        // Process the data
        await _userService.AddAsync(entity3);


        return Ok();
    }
}
