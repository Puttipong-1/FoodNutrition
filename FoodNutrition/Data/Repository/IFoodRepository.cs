using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository
{
    public interface IFoodRepository:IRepositoryBase<Food>
    {
        Task<Food> GetFoodById(int id);
        Task<Food> GetFoodByIdAndPortion(int foodId,int portionId);
        Task<List<Food>> GetFoodsByName(string name);
    }
}
