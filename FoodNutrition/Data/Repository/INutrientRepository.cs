using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository
{
    public interface INutrientRepository:IRepositoryBase<Nutrient>
    {
        Task<Nutrient> GetNutrientById(int id);
    }
}
