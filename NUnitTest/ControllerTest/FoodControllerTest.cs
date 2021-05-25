using FoodNutrition.Controllers;
using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.ControllerTest
{
    [TestFixture]
    class FoodControllerTest
    {
        private FoodController foodController;
        private Mock<IFoodService> mockFoodService;
        [OneTimeSetUp]
        public void Setup()
        {
            mockFoodService = new Mock<IFoodService>(MockBehavior.Strict);
            foodController = new FoodController(mockFoodService.Object);
        }
        [Test]
        public async Task SearchFoodSuccess()
        {
            mockFoodService.Setup(x => x.SearchByFoodName(It.IsAny<string>()))
                .ReturnsAsync(GetFakeSearchs());

            var actionResult = await foodController.SearchFoodByName("");

            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var result = (OkObjectResult)actionResult;
            var searches = (List<SearchResult>)result.Value;
            Assert.AreEqual(4, searches.Count);
        }
        [Test]
        public async Task SearchFoodFail()
        {
            mockFoodService.Setup(x => x.SearchByFoodName(It.IsAny<string>()))
                .ReturnsAsync(new List<SearchResult>());

            var actionResult = await foodController.SearchFoodByName("");

            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var result = (OkObjectResult)actionResult;
            var searches = (List<SearchResult>)result.Value;
            Assert.AreEqual(0, searches.Count);
        }
        [Test]
        public async Task GetFoodByIdSuccess()
        {
            mockFoodService.Setup(x => x.GetFoodById(It.IsAny<int>()))
                .ReturnsAsync(GetFakeFoodResult());

            var actionResult = await foodController.GetFoodById(1);

            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var result = (OkObjectResult)actionResult;
            var food = (FoodResult)result.Value;
            Assert.AreEqual(1, food.FoodId);
            Assert.AreEqual("steak", food.Name);
            Assert.AreEqual(3, food.Nutrients.Count);
            Assert.AreEqual(2, food.Portions.Count);
        }
        [Test]
        public async Task GetFoodByIdFail()
        {
            mockFoodService.Setup(x => x.GetFoodById(It.IsAny<int>()))
                .ReturnsAsync((FoodResult)null);

            var actionResult = await foodController.GetFoodById(1);

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
            var result = (BadRequestObjectResult)actionResult;
            var msg = (string)result.Value;
            Assert.AreEqual("Food not found", msg);
        }
        [Test]
        public async Task GetFoodNutrientSuccess()
        {
            mockFoodService.Setup(x => x.CalculateFoodNutrient(It.IsAny<FoodNutrientPortion>()))
                .ReturnsAsync(GetFakeFoodNutrientResult);

            var actionResult = await foodController.CalculateFoodPortion(new FoodNutrientPortion { });

            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var result = (OkObjectResult)actionResult;
            var fnr = (FoodNutrientResult)result.Value;
            Assert.AreEqual(1, fnr.FoodId);
            Assert.AreEqual(3, fnr.Nutrients.Count);
        }
        [Test]
        public async Task GetFoodNutrientFail()
        {
            mockFoodService.Setup(x => x.CalculateFoodNutrient(It.IsAny<FoodNutrientPortion>()))
                .ReturnsAsync((FoodNutrientResult)null);

            var actionResult = await foodController.CalculateFoodPortion(new FoodNutrientPortion { });

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
            var result = (BadRequestObjectResult)actionResult;
            var msg = (string)result.Value;
            Assert.AreEqual("Food not found", msg);

        }
        private FoodNutrientResult GetFakeFoodNutrientResult()
        {
            return new FoodNutrientResult
            {
                FoodId = 1,
                Name = "steak",
                Amount = 5,
                Nutrients = new List<Nutrients>
                {
                    new Nutrients { Name = "Water", Amount = 15.0, Unit = "ml" },
                    new Nutrients { Name = "Protein", Amount = 23.0, Unit = "g" },
                    new Nutrients { Name = "Sugar", Amount = 55.0, Unit = "g" }
                },
                Portion = "14 piece"
            };
        }
        private FoodResult GetFakeFoodResult()
        {
            return new FoodResult
            {
                FoodId = 1,
                Name = "steak",
                Category = "category",
                Attribute = "attribute",
                Nutrients = new List<Nutrients>
                {
                    new Nutrients { Name = "Water", Amount = 15.0, Unit = "ml" },
                    new Nutrients { Name = "Protein", Amount = 23.0, Unit = "g" },
                    new Nutrients { Name = "Sugar", Amount = 55.0, Unit = "g" }
                },
                Portions =new List<Portions>{
                    new Portions { PortionId = 1, Amount = "5", Description = "test", Gram = 66 },
                    new Portions { PortionId = 2, Amount = "6", Description = "test2", Gram = 55 }
                }
            };
        }
        private List<SearchResult> GetFakeSearchs()
        {
            return new List<SearchResult>
            {
                new SearchResult{Id=1,Name="a",AdditionDescription="a",Category="a",CategoryCode=100},
                new SearchResult{Id=1,Name="b",AdditionDescription="b",Category="b",CategoryCode=100},
                new SearchResult{Id=1,Name="c",AdditionDescription="c",Category="c",CategoryCode=100},
                new SearchResult{Id=1,Name="d",AdditionDescription="d",Category="d",CategoryCode=100}
            };
        }
    }
}
