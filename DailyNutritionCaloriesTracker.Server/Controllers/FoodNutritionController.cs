using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NT.Application.Contracts.Entities;
using NT.Application.Contracts.Ports;

namespace DailyNutritionCaloriesTracker.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodNutritionController : ControllerBase
{

    private readonly ILogger<FoodNutritionController> _logger;
    private readonly IFoodNutritionDataHandler _foodNutritionService;
    private readonly IMapper _mapper;

    public FoodNutritionController(ILogger<FoodNutritionController> logger,
        IFoodNutritionDataHandler foodNutritionService,
        IMapper mapper)
    {
        _logger = logger;
        _foodNutritionService = foodNutritionService;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetFoodNutrition")]
    public IEnumerable<FoodNutritionDto> Get()
    {
        return LoadData();
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

            var foodNutritionEntity = MapDtoToEntity(foodNutrition);
            foodNutritionEntities.Add(foodNutritionEntity);
        }


        //add the list of FoodNutritionEntity to the database
        //foreach (FoodNutritionEntity foodNutrition in foodNutritionEntities)
        //{
        //    _foodNutritionService.AddAsync(foodNutrition);
        //}

        return foodNutritionList;
    }

    private FoodNutritionEntity MapDtoToEntity(FoodNutritionDto dto)
    {
        var entity = new FoodNutritionEntity
        {
            // Assuming Id is generated elsewhere or not needed in this context
            Name = dto.Name,
            Measurement = dto.Measurement,
            Calories = dto.Calories,
            Protein = dto.Protein,
            Carbs = dto.Carbs,
            Fat = dto.Fat
        };

        return entity;
    }

    private FoodNutritionDto MapEntityToDto(FoodNutritionEntity entity)
    {
        var dto = new FoodNutritionDto
        {
            Name = entity.Name,
            Measurement = entity.Measurement,
            Calories = entity.Calories,
            Protein = entity.Protein,
            Carbs = entity.Carbs,
            Fat = entity.Fat
        };

        return dto;
    }

}
