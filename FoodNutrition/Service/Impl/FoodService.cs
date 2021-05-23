using FoodNutrition.Data;
using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using FoodNutrition.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Service.Impl
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository foodRepository;
        public FoodService(IFoodRepository _foodRepository)
        {
            foodRepository = _foodRepository;
        }
        public async Task<FoodNutrientResult> CalculateFoodNutrient(FoodNutrientPortion fnp)
        {
                Food food = await foodRepository.GetFoodByIdAndPortion(fnp.FoodId, fnp.PortionId);
                if (food is null) return null;
                return new FoodNutrientResult(fnp,food);
        }

        public async Task<FoodResult> GetFoodById(int id)
        {
            Food food = await foodRepository.GetFoodById(id);
            if (food is null) return null;
            return new FoodResult(food);
        }

        public async Task<List<SearchResult>> SearchByFoodName(string name)
        {
            List<Food> foods = await foodRepository.GetFoodsByName(name);
            if (foods.Count==0) return null;
            List<SearchResult> searches=new List<SearchResult>();
            foreach(Food f in foods)
            {
                searches.Add(new SearchResult(f));
            }
            return searches;
        }
    }
}
