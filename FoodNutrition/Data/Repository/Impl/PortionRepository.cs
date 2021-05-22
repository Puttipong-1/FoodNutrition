using FoodNutrition.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository.Impl
{
    public class PortionRepository:RepositoryBase<Portion>,IPortionRepository
    {
        public PortionRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<Portion> GetPortionById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.PortionId == id);
        }
    }
}
