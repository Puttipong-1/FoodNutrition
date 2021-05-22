using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository
{
    public interface ICategoryRepository:IRepositoryBase<Category>
    {
        Task<Category> GetCategoryById(int id);
        Task<Category> GetCategoryByCode(int code);
        Task<List<Category>> GetAllCategory();
    }
}
