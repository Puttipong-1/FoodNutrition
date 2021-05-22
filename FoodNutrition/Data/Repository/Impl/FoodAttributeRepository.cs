using FoodNutrition.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository.Impl
{
    public class FoodAttributeRepository:RepositoryBase<FoodAttribute>,IFoodAttributeRepository
    {
        public FoodAttributeRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<List<FoodAttribute>> GetByFoodId(int id)
        {
            return await GetAll().Where(f => f.FoodId == id).ToListAsync();
        }

        public async Task<FoodAttribute> GetById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(f => f.FoodAttributeId == id);
        }
    }
}
