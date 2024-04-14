using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NT.Application.Contracts.Entities;
using NT.Application.Services.Abstractions;

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

        List<FoodLogDto> foodlogs = new List<FoodLogDto>
        {
            new FoodLogDto { DateTime = new DateTime(2023, 04, 10), TotalCalories = 2000, TotalCarbs = 50, TotalProtein = 100, TotalFat = 70 },
            new FoodLogDto { DateTime = new DateTime(2023, 04, 11), TotalCalories = 1800, TotalCarbs = 45, TotalProtein = 120, TotalFat = 60 }
        };
        return foodlogs;
        //return LoadData();
    }

    [HttpPost("createfoodlog")]
    public IActionResult Post([FromBody] FoodLogDto foodLogDto)
    {
        //insert the FoodLogDto to the database by calling the service
        //FoodLogEntity entity = _mapper.Map<FoodLogEntity>(foodLogDto);

        //FoodLogDto foodLogDto = request.FoodLogDto;
        _logger.LogInformation("Received data: {@FoodLog}", foodLogDto);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid data: {@ModelStateErrors}", ModelState);
            return BadRequest(ModelState);
        }

        FoodLogEntity res = MapToEntity(foodLogDto);

        // Process the data
        return Ok();



        //_foodLogService.AddAsync(entity);

    }

    public class FoodLogDtoRequest
    {
        public required FoodLogDto FoodLogDto { get; set; }
    }

    private FoodLogEntity MapToEntity(FoodLogDto dto)
    {
        IEnumerable<FoodItemEntity> foodItems = dto.FoodItems.Select(_mapper.Map<FoodItemEntity>);

        FoodLogEntity logEntity = new FoodLogEntity();
        logEntity.DateTime = dto.DateTime;

        logEntity.UserId = dto.UserId;

        logEntity.FoodItems = foodItems;




        return logEntity;
        //throw new NotImplementedException();
    }


}
