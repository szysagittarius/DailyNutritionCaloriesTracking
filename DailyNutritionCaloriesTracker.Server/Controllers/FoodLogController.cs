using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NT.Application.Contracts.Entities;
using NT.Application.Services.Abstractions;
using NutritionTracker.Api.Models;
using NutritionTracker.Api.Profilers;

namespace DailyNutritionCaloriesTracker.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodLogController : ControllerBase
{

    private readonly ILogger<FoodLogController> _logger;
    private readonly IFoodLogService _foodLogService;
    private readonly IMapper _mapper;

    public FoodLogController(ILogger<FoodLogController> logger,
        IFoodLogService foodLogService,
        IMapper mapper)
    {
        _logger = logger;
        _foodLogService = foodLogService;
        _mapper = mapper;
    }

    [HttpGet("GetFoodLogs")]
    public IEnumerable<FoodLogDto> Get()
    {
        //load FoodLogDto from database by calling the service
        Task<IEnumerable<FoodLogEntity>> entities = _foodLogService.GetAllAsync();

        //return _mapper.Map<IEnumerable<FoodLogDto>>(entities);
        //logs:
        //    [
        //    { date: '2023-04-10', calories: 2000, carbs: '50g', protein: '100g', fat: '70g' },
        //    { date: '2023-04-11', calories: 1800, carbs: '45g', protein: '120g', fat: '60g' },
        //    // Add more logs here
        //  ]

        FoodItemDto foodItem1 = new FoodItemDto { FoodNutritionId = Guid.NewGuid(), Unit = 1, FoodLogId = Guid.NewGuid() };
        LinkedList<FoodItemDto> foodItems = new LinkedList<FoodItemDto>();
        foodItems.AddLast(foodItem1);

        List<FoodLogDto> foodlogs = new List<FoodLogDto>
        {
            new FoodLogDto { DateTime = new DateTime(2023, 04, 10), TotalCalories = 2000, TotalCarbs = 50, TotalProtein = 100, TotalFat = 70, FoodItems = foodItems },
            new FoodLogDto { DateTime = new DateTime(2023, 04, 11), TotalCalories = 1800, TotalCarbs = 45, TotalProtein = 120, TotalFat = 60, FoodItems = foodItems}
        };
        return foodlogs;
        //return LoadData();
    }

    [HttpPost("createfoodlog")]
    public async Task<IActionResult> PostAsync([FromBody] FoodLogDto foodLogDto)
    {
        //insert the FoodLogDto to the database by calling the service

        //1
        MapperConfiguration dtoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new FoodNutritionDtoProfiler());
            cfg.AddProfile(new FoodItemDtoProfiler());
            cfg.AddProfile(new FoodLogDtoProfiler());
            cfg.AddProfile(new UserDtoProfiler());
        });

        IMapper dtoMapper = dtoMapperConfig.CreateMapper();

        FoodLogEntity entity3 = dtoMapper.Map<FoodLogEntity>(foodLogDto);

        //2 need to fix bug here, on
        //FoodLogEntity entity = _mapper.Map<FoodLogEntity>(foodLogDto);


        //temp bypass, need to covert to real after user controller added
        entity3.UserId = Guid.Parse("2c82025f-f351-4246-aaff-21301ec71803");

        //UI work need to include the guid, it will be removed
        foreach (FoodItemEntity item in entity3.FoodItems)
        {
            item.FoodNutritionId = Guid.Parse("B83D0C39-DBEA-44C8-EC45-08DC5AC3A936");
        }

        // Process the data
        await _foodLogService.AddAsync(entity3);


        return Ok();
    }
}
