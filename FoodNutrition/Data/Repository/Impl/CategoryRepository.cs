using FoodNutrition.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository.Impl
{
    public class CategoryRepository:RepositoryBase<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<List<Category>> GetAllCategory()
        {
            return await GetAll().OrderBy(c => c.Code).ToListAsync();
        }

        public async Task<Category> GetCategoryByCode(int code)
        {
            return await GetAll().Where(c => c.Code == code)
                .Include(x=>x.Foods)
                .FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await GetAll().Where(c => c.CategoryId == id)
                .Include(x => x.Foods)
                .FirstOrDefaultAsync();
        }
    }
}
