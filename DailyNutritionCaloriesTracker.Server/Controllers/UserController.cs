using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NT.Application.Contracts.Entities;
using NT.Application.Services.Abstractions;
using NutritionTracker.Api.Models;

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
