using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Service
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<List<SearchResult>> GetFoodsinCategory(int code);
    }
}
