using FoodNutrition.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository.Impl
{
    public class FoodRepository:RepositoryBase<Food>,IFoodRepository
    {
        public FoodRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<Food> GetFoodById(int id)
        {
            return await GetAll().Where(x=>x.FoodId==id)
                .Include(f => f.Category)
                .Include(f => f.FoodNutrients)
                .ThenInclude(f => f.Nutrient)
                .Include(f => f.Portions)
                .FirstOrDefaultAsync();
        }

        public async Task<Food> GetFoodByIdAndPortion(int foodId, int portionId)
        {
            return await GetAll().Where(x => x.FoodId == foodId)
                .Include(x => x.FoodNutrients)
                .ThenInclude(x => x.Nutrient)
                .Include(x => x.Portions.Where(x=>x.PortionId==portionId))
                .FirstOrDefaultAsync();
        }
        public async Task<List<Food>> GetFoodsByName(string name)
        {
            return await GetAll().Where(f => f.Name.ToLower().Contains(name))
                .Include(f => f.Category)
                .Include(f => f.FoodAttributes)
                .ToListAsync();
        }
    }
}
