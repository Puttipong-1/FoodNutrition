using FoodNutrition.Controllers;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
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
    class CategoryControllerTest
    {
        private CategoryController categoryController;
        private Mock<ICategoryService> mockCategoryService;
        [OneTimeSetUp]
        public void Setup()
        {
            mockCategoryService = new Mock<ICategoryService>(MockBehavior.Strict);
            categoryController = new CategoryController(mockCategoryService.Object);
        }
        //Get all categories
        [Test]
        public async Task GetAllCategories()
        {
            mockCategoryService.Setup(x => x.GetAllCategories())
                .ReturnsAsync(GetFakeCategories());

            var actionResult = await categoryController.GetAllCategories();
            
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var result = (OkObjectResult)actionResult;
            var categories = (List<Category>)result.Value;
            Assert.AreEqual(4, categories.Count);
        } 
        //Get foods in category success
        [Test]
        public async Task GetFoodsinCategorySuccess()
        {
            mockCategoryService.Setup(x => x.GetFoodsinCategory(It.IsAny<int>()))
                .ReturnsAsync(GetFakeSearchs());

            var actionResult = await categoryController.GetCategoryByCode(100);

            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var result = (OkObjectResult)actionResult;
            var searchs = (List<SearchResult>)result.Value;
            Assert.AreEqual(4, searchs.Count);
        }
        //Get foods in category fail
        [Test]
        public async Task GetFoodsinCategoryFail()
        {
            mockCategoryService.Setup(x => x.GetFoodsinCategory(It.IsAny<int>()))
                .ReturnsAsync((List<SearchResult>)null);

            var actionResult = await categoryController.GetCategoryByCode(100);

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
            var result = (BadRequestObjectResult)actionResult;
            var msg = (string)result.Value;
            Assert.AreEqual("Category is not found", msg);
        }
        private List<Category> GetFakeCategories()
        {
            return new List<Category>
            {
                new Category{CategoryId=1,Code=100,Description="A"},
                new Category{CategoryId=2,Code=200,Description="B"},
                new Category{CategoryId=3,Code=300,Description="C"},
                new Category{CategoryId=4,Code=400,Description="D"}
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
