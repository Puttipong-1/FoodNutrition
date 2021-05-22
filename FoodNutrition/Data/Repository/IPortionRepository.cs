using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository
{
    public interface IPortionRepository:IRepositoryBase<Portion>
    {
        Task<Portion> GetPortionById(int id);
    }
}
