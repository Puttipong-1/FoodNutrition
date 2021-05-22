using FoodNutrition.Data;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using FoodNutrition.Data.Repository;
using FoodNutrition.Service;
using FoodNutrition.Service.Impl;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUnitTest.ServiceTest
{
    [TestFixture]
    public class CategoryServiceTest
    {

        private ICategoryService categoryService;
        Mock<ICategoryRepository> mockCategoryRepository;
        [OneTimeSetUp]
        public void Setup()
        {
            mockCategoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryService = new CategoryService(mockCategoryRepository.Object);
        }
        [Test]
        public async Task GetAllCategoriesSuccess()
        {
            mockCategoryRepository.Setup(x => x.GetAllCategory())
                .ReturnsAsync(GetFakeCategories());
            List<Category> actual = await categoryService.GetAllCategories();
            Assert.AreEqual(4, actual.Count);
        }
        //Found Category and food is not empty
        [Test]
        public async Task GetCategoryByCodeSuccess()
        {
            mockCategoryRepository.Setup(x => x.GetCategoryByCode(It.IsAny<int>()))
                .ReturnsAsync(GetFakeCategory());
            List<SearchResult> searches = await categoryService.GetFoodsinCategory(200);
            Assert.IsNotNull(searches);
            Assert.Greater(searches.Count, 0);
            Assert.AreEqual(1, searches[0].Id);
        }
        //Found Category and food is empty
        [Test]
        public async Task GetCategoryByCodeSuccess2()
        {
            mockCategoryRepository.Setup(x => x.GetCategoryByCode(It.IsAny<int>()))
                .ReturnsAsync(new Category { CategoryId = 2, Code = 200, Description = "Steak" });
            List<SearchResult> searches = await categoryService.GetFoodsinCategory(200);
            Assert.IsNotNull(searches);
            Assert.AreEqual(searches.Count, 0);
        }
        //Not found Category
        [Test]
        public async Task GetCategoryByCodeFailure()
        {
            mockCategoryRepository.Setup(x => x.GetCategoryByCode(It.IsAny<int>()))
                .ReturnsAsync((Category)null);
            List<SearchResult> searches = await categoryService.GetFoodsinCategory(100);
            Assert.IsNull(searches);
        }
        private Category GetFakeCategory()
        {
            List<Food> foods = new List<Food>
            {
                new Food { FoodId = 1, CategoryId = 2, Name = "Pork steak" },
                new Food { FoodId = 2, CategoryId = 2, Name = "Salmond steak" },
                new Food { FoodId = 3, CategoryId = 2, Name = "Chicken steak" },
            };
            return new Category {
                CategoryId = 2,
                Code = 200,
                Description = "Steak",
                Foods=foods
            };
        }
        private List<Category> GetFakeCategories()
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category { CategoryId = 1, Code = 100, Description = "Soup" });
            categories.Add(new Category { CategoryId = 2, Code = 200, Description = "Steak" });
            categories.Add(new Category { CategoryId = 3, Code = 300, Description = "Rice" });
            categories.Add(new Category { CategoryId = 4, Code = 400, Description = "Dessert" });
            return categories;
        }
    }
}