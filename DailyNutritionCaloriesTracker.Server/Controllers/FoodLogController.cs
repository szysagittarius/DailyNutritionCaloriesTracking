using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NT.Application.Contracts.Entities;
using NT.Application.Contracts.Ports;
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
        var entities =  _foodLogService.GetAllAsync();

        //return _mapper.Map<IEnumerable<FoodLogDto>>(entities);
        //logs:
        //    [
        //    { date: '2023-04-10', calories: 2000, carbs: '50g', protein: '100g', fat: '70g' },
        //    { date: '2023-04-11', calories: 1800, carbs: '45g', protein: '120g', fat: '60g' },
        //    // Add more logs here
        //  ]

        var foodlogs = new List<FoodLogDto>
        {
            new FoodLogDto { DateTime = new DateTime(2023, 04, 10), Calories = 2000, Carbs = 50, Protein = 100, Fat = 70 },
            new FoodLogDto { DateTime = new DateTime(2023, 04, 11), Calories = 1800, Carbs = 45, Protein = 120, Fat = 60 }
        };
        return foodlogs;
        //return LoadData();
    }

    [HttpPost(Name = "CreateFoodLog")]
    public void Post(FoodLogDto foodLogDto)
    {
       //insert the FoodLogDto to the database by calling the service
       var entity = _mapper.Map<FoodLogEntity>(foodLogDto);
        _foodLogService.AddAsync(entity);

    }


    //if this changes to public, it will mess up the swagger documentation
    private IEnumerable<FoodNutritionDto> LoadData()
    {
        //load data from json file, under App_data folder in the project
        string json = System.IO.File.ReadAllText("App_Data/food_nutritional_values.json");

        Dictionary<string, FoodNutritionDto>? foodNutritionDictionary = JsonConvert.DeserializeObject<Dictionary<string, FoodNutritionDto>>(json);

        List<FoodNutritionDto> foodNutritionList = foodNutritionDictionary.Select(kvp =>
        {
            FoodNutritionDto foodNutrition = kvp.Value;
            //foodNutrition.Id = Guid.NewGuid();
            foodNutrition.Name = kvp.Key;
            foodNutrition.Measurement = "per 100g";
            return foodNutrition;
        }).ToList();

        //Imapper map the list of FoodNutritionDto to list of FoodNutritionEntity
        List<FoodNutritionEntity> foodNutritionEntities = new List<FoodNutritionEntity>();
        
        foreach (FoodNutritionDto foodNutrition in foodNutritionList)
        {
            //FoodNutritionEntity foodNutritionEntity = _mapper.Map<FoodNutritionEntity>(foodNutrition);

            //var foodNutritionEntity = MapDtoToEntity(foodNutrition);
            //foodNutritionEntities.Add(foodNutritionEntity);
        }


        //add the list of FoodNutritionEntity to the database
        //foreach (FoodLogEntity foodNutrition in foodNutritionEntities)
        //{
        //    _foodLogService.AddAsync(foodNutrition);
        //}

        return foodNutritionList;
    }


}
