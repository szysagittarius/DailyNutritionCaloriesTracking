using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NT.Application.Contracts.Entities;
using NT.Application.Services.Abstractions;
using NutritionTracker.Api.Models;
using NutritionTracker.Api.Profilers;

namespace DailyNutritionCaloriesTracker.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<FoodLogController> _logger;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(ILogger<FoodLogController> logger,
        IUserService userService,
        IMapper mapper)
    {
        _logger = logger;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("GetUsers")]
    public async Task<IEnumerable<UserDto>> GetAsync()
    {
        //load FoodLogDto from database by calling the service
        IEnumerable<UserEntity> entities = await _userService.GetAllAsync();

        MapperConfiguration dtoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserDtoProfiler());
        });

        IMapper dtoMapper = dtoMapperConfig.CreateMapper();

        return entities.Select(e => dtoMapper.Map<UserDto>(e));

    }

    [HttpPost("createUser")]
    public IActionResult Post([FromBody] UserDto user)
    {
        //insert the FoodLogDto to the database by calling the service

        InsertDefaultDataAsync();

        return Ok();
    }


    private async Task InsertDefaultDataAsync()
    {
        MapperConfiguration dtoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserDtoProfiler());
        });

        IMapper dtoMapper = dtoMapperConfig.CreateMapper();

        //load data from json file, under App_data folder in the project
        string json = System.IO.File.ReadAllText("App_Data/user_records.json");

        List<UserDto>? users = JsonConvert.DeserializeObject<List<UserDto>>(json);

        UserDto? userDto = users.FirstOrDefault();


        UserEntity userEntity = dtoMapper.Map<UserEntity>(userDto);

        // Process the data
        try
        {
            await _userService.AddAsync(userEntity);
        }
        catch (Exception)
        {

            throw;
        }

    }
}
