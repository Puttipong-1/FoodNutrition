using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Service
{
    public interface IFoodService
    {
        Task<List<SearchResult>> SearchByFoodName(string name);
        Task<Food> GetFoodById(int id);
        Task<FoodNutrientResult> CalculateFoodNutrient(FoodNutrientPortion foodNutrientPortion);
    }
}
