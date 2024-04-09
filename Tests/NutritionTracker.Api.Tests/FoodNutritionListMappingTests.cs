using AutoMapper;
using Newtonsoft.Json;
using NT.Application.Contracts.Entities;
using NutritionTracker.Api.Profilers;

namespace NutritionTracker.Api.Tests;

public class FoodNutritionListMappingTests
{
    private IMapper _mapper;
    [SetUp]
    public void Setup()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<FoodNutritionDtoProfiler>(); // Assuming your profile class is named FoodNutritionDtoProfiler
        });

        _mapper = configuration.CreateMapper();
    }

    [Test]
    public void EntityToDtoMapping_IsCorrect()
    {
        // Arrange
        var entity = new FoodNutritionEntity
        {
            //Id = Guid.NewGuid(),
            Name = "Apple",
            Measurement = "100g",
            Calories = 52,
            Protein = 0.3,
            Carbs = 14,
            Fat = 0.2
        };

        // Act
        var dto = _mapper.Map<FoodNutritionDto>(entity);

        // Assert
        Assert.AreEqual(entity.Name, dto.Name);
        Assert.AreEqual(entity.Measurement, dto.Measurement);
        Assert.AreEqual(entity.Calories, dto.Calories);
        Assert.AreEqual(entity.Protein, dto.Protein);
        Assert.AreEqual(entity.Carbs, dto.Carbs);
        Assert.AreEqual(entity.Fat, dto.Fat);
    }

    [Test]
    public void DtoToEntityMapping_IsCorrect()
    {
        // Arrange
        var dto = new FoodNutritionDto
        {
            Name = "Banana",
            Measurement = "100g",
            Calories = 89,
            Protein = 1.1,
            Carbs = 23,
            Fat = 0.3
        };

        // Act
        var entity = _mapper.Map<FoodNutritionEntity>(dto);

        // Assert
        Assert.AreEqual(dto.Name, entity.Name);
        Assert.AreEqual(dto.Measurement, entity.Measurement);
        Assert.AreEqual(dto.Calories, entity.Calories);
        Assert.AreEqual(dto.Protein, entity.Protein);
        Assert.AreEqual(dto.Carbs, entity.Carbs);
        Assert.AreEqual(dto.Fat, entity.Fat);
    }

    [Test]
    public void ListOfDtoToEntityMapping_IsCorrect()
    {
        // Arrange
        var foodNutritionList = new List<FoodNutritionDto>
            {
                new FoodNutritionDto
                {
                    Name = "Apple",
                    Measurement = "100g",
                    Calories = 52,
                    Protein = 0.3,
                    Carbs = 14,
                    Fat = 0.2
                },
                new FoodNutritionDto
                {
                    Name = "Banana",
                    Measurement = "100g",
                    Calories = 89,
                    Protein = 1.1,
                    Carbs = 23,
                    Fat = 0.3
                }
            };

        // Act
        List<FoodNutritionEntity> foodNutritionEntities = _mapper.Map<List<FoodNutritionEntity>>(foodNutritionList);

        // Assert
        Assert.AreEqual(foodNutritionList.Count, foodNutritionEntities.Count, "The mapped list has a different number of items.");

        for (int i = 0; i < foodNutritionList.Count; i++)
        {
            Assert.AreEqual(foodNutritionList[i].Name, foodNutritionEntities[i].Name, $"Item {i} Name does not match.");
            Assert.AreEqual(foodNutritionList[i].Measurement, foodNutritionEntities[i].Measurement, $"Item {i} Measurement does not match.");
            Assert.AreEqual(foodNutritionList[i].Calories, foodNutritionEntities[i].Calories, $"Item {i} Calories does not match.");
            Assert.AreEqual(foodNutritionList[i].Protein, foodNutritionEntities[i].Protein, $"Item {i} Protein does not match.");
            Assert.AreEqual(foodNutritionList[i].Carbs, foodNutritionEntities[i].Carbs, $"Item {i} Carbs does not match.");
            Assert.AreEqual(foodNutritionList[i].Fat, foodNutritionEntities[i].Fat, $"Item {i} Fat does not match.");
        }
    }

    [Test]
    public void ListOfDtoToEntityMappingFromFile_IsCorrect() 
    {
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
            FoodNutritionEntity foodNutritionEntity = _mapper.Map<FoodNutritionEntity>(foodNutrition);
            foodNutritionEntities.Add(foodNutritionEntity);
        }

        Assert.AreEqual(15, foodNutritionEntities.Count, $"Item counts is not 15.");

    }

}