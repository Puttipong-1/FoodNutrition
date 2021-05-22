using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository
{
    public interface IFoodAttributeRepository:IRepositoryBase<FoodAttribute>
    {
        Task<FoodAttribute> GetById(int id);
        Task<List<FoodAttribute>> GetByFoodId(int id);
    }
}
