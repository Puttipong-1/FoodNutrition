using FoodNutrition.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository.Impl
{
    public class FoodNutrientRepository:RepositoryBase<FoodNutrient>,IFoodNutrientRepository
    {
        public FoodNutrientRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<FoodNutrient> GetById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(f => f.FoodNutrientId == id);
        }
    }
}
