using FoodNutrition.Data;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Service.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                return await dbContext.Categories
                    .OrderBy(c => c.Code)
                    .ToListAsync();
            }catch(Exception e)
            {
                throw;
            }
        }

        public async Task<List<SearchResult>> GetFoodsinCategory(int code)
        {
            try
            {
                Category category = await dbContext.Categories
                    .Where(c => c.Code == code)
                    .Include(c => c.Foods)
                    .ThenInclude(f => f.FoodAttributes)
                    .SingleAsync();
                List<SearchResult> searches = new List<SearchResult>();
                if (category.Foods != null && category.Foods.Any())
                {
                    foreach (Food f in category.Foods)
                    {
                        searches.Add(new SearchResult(f,category));
                    }
                    return searches;
                }
                return searches;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
