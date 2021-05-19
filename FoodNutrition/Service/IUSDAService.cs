using FoodNutrition.Data.DTO.USDA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Service
{
    public interface IUSDAService
    {
        Task AddAttribute(List<Data.DTO.USDA.Attribute> attributes);
        Task AddCategory(List<Category> categories);
        Task AddFood(List<Food> foods);
        Task AddFoodNutrient(List<FoodNutrient> foodNutrients);
        Task AddNutrient(List<Nutrient> nutrients);
        Task AddPortion(List<Portion> portions);
    }
}
