using FoodNutrition.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository.Impl
{
    public class NutrientRepository:RepositoryBase<Nutrient>,INutrientRepository
    {
        public NutrientRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<Nutrient> GetNutrientById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x=>x.NutrientId==id);
        }
    }
}
