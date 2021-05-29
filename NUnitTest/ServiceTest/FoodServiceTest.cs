using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.Model;
using FoodNutrition.Data.Repository;
using FoodNutrition.Service;
using FoodNutrition.Service.Impl;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.ServiceTest
{
    [TestFixture]
    class FoodServiceTest
    {
        private IFoodService foodService;
        private Mock<IFoodRepository> mockFoodRepository;
        [OneTimeSetUp]
        public void Setup()
        {
            mockFoodRepository = new Mock<IFoodRepository>(MockBehavior.Strict);
            foodService = new FoodService(mockFoodRepository.Object);
        }
        //Calculate food nutrient and portion is not empty
        [Test]
        public async Task CalculateFoodNutrientSuccess()
        {
            Food food = GetFakeFood();
            FoodNutrientPortion fnp = GetFakeFoodNutrientPortion();
            mockFoodRepository.Setup(x => x.GetFoodByIdAndPortion(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(food);

            FoodNutrientResult result = await foodService.CalculateFoodNutrient(fnp);
            double gram = (fnp.Amount*food.Portions[0].Gram)/100;

            Assert.AreEqual(food.FoodId, result.FoodId);
            Assert.AreEqual(food.Name, result.Name);
            Assert.AreEqual(food.FoodNutrients.Count, result.Nutrients.Count);
            for(int i = 0; i < food.FoodNutrients.Count; i++)
            {
                double nut= Math.Round(gram * food.FoodNutrients[i].Amount, 2);
                Assert.AreEqual(nut, result.Nutrients[i].Amount);
            }
        }
        //Calculate food nutrient and portion is empty
        [Test]
        public async Task CalculateFoodNutrientSuccess2()
        {
            Food food = GetFakeFood();
            FoodNutrientPortion fnp = GetFakeFoodNutrientPortion();
            food.Portions = new List<Portion>();
            mockFoodRepository.Setup(x => x.GetFoodByIdAndPortion(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(food);

            FoodNutrientResult result = await foodService.CalculateFoodNutrient(fnp);
            double gram = fnp.Amount * 1.0;

            Assert.AreEqual(food.FoodId, result.FoodId);
            Assert.AreEqual(food.Name, result.Name);
            Assert.AreEqual(food.FoodNutrients.Count, result.Nutrients.Count);
            for (int i = 0; i < food.FoodNutrients.Count; i++)
            {
                double nut = Math.Round(gram * food.FoodNutrients[i].Amount, 2);
                Assert.AreEqual(nut, result.Nutrients[i].Amount);
            }
        }
        //Calculate food nutrient fail(food not found)
        [Test]
        public async Task CalculateFoodNutrientFail()
        {
            FoodNutrientPortion fnp = GetFakeFoodNutrientPortion();
            mockFoodRepository.Setup(x => x.GetFoodByIdAndPortion(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((Food)null);

            FoodNutrientResult result = await foodService.CalculateFoodNutrient(fnp);

            Assert.IsNull(result);
        }
        //Get food by id success
        [Test]
        public async Task GetByFoodIdSuccess()
        {
            Food food = GetFakeFood();
            mockFoodRepository.Setup(x => x.GetFoodById(It.IsAny<int>()))
                .ReturnsAsync(food);

            Food result = await foodService.GetFoodById(1);

            Assert.AreEqual(food.FoodId, result.FoodId);
            Assert.AreEqual(food.Name, result.Name);
            Assert.AreEqual(food.Category.Description, result.Category.Description);
            Assert.AreEqual(food.Portions.Count, result.Portions.Count);
            Assert.AreEqual(food.FoodNutrients.Count, result.FoodNutrients.Count);
        }
        //Get food by id fail
        [Test]
        public async Task GetByFoodIdFail()
        {
            mockFoodRepository.Setup(x => x.GetFoodById(It.IsAny<int>()))
                .ReturnsAsync((Food)null);

            Food result = await foodService.GetFoodById(1);

            Assert.IsNull(result);
        }//Search food by name success
        [Test]
        public async Task SearchFoodSuccess()
        {
            List<Food> foods = GetFakeFoodList();
            mockFoodRepository.Setup(x => x.GetFoodsByName(It.IsAny<string>()))
                .ReturnsAsync(foods);

            List<SearchResult> results = await foodService.SearchByFoodName("1");

            Assert.AreEqual(foods.Count, results.Count);
        }
        //Search food by name fail
        [Test]
        public async Task SearchFoodFail()
        {
            mockFoodRepository.Setup(x => x.GetFoodsByName(It.IsAny<string>()))
                .ReturnsAsync(new List<Food>());

            List<SearchResult> results = await foodService.SearchByFoodName("1");

            Assert.IsNull(results);
        }
        private FoodNutrientPortion GetFakeFoodNutrientPortion()
        {
            return new FoodNutrientPortion
            {
                FoodId = 1,
                PortionId = 1,
                Amount = 2
            };
        }
        private Food GetFakeFood()
        {
            Category category = new Category
            {
                CategoryId = 1,
                Code = 100,
                Description = "Steak"
            };
            List<FoodAttribute> foodAttributes = new List<FoodAttribute>
            {
                new FoodAttribute{FoodAttributeId=1,FoodId=1,Name="aaa",SeqNum=0,Value="11"},
                new FoodAttribute{FoodAttributeId=2,FoodId=1,Name="bbb",SeqNum=1,Value="22"}
            };
            List<FoodNutrient> foodNutrients = new List<FoodNutrient>
            {
                new FoodNutrient{FoodNutrientId=1,FoodId=1,Amount=10.0,NutrientId=1,
                    Nutrient=new Nutrient{NutrientId=1,Name="A",Unit="G"}
                },
                new FoodNutrient{FoodNutrientId=2,FoodId=1,Amount=20.0,NutrientId=2,
                    Nutrient=new Nutrient{NutrientId=2,Name="B",Unit="ml"}
                }
            };
            List<Portion> portions = new List<Portion>
            {
                new Portion {PortionId=1,Amount="15 g",FoodId=1,Description="Test",Gram=150},
                new Portion {PortionId=1,Amount="15 g",FoodId=1,Description="Test",Gram=200}
            };
            return new Food
            {
                FoodId = 1,
                Name = "Pork steak",
                CategoryId = 1,
                Category = category,
                FoodAttributes = foodAttributes,
                FoodNutrients = foodNutrients,
                Portions = portions
            };
        }
        private List<Food> GetFakeFoodList()
        {
            return new List<Food>
            {
                new Food{FoodId=1,Name="1",Category=null},
                new Food{FoodId=2,Name="2",Category=null},
                new Food{FoodId=3,Name="3",Category=null}
            };
        }
     }
}
