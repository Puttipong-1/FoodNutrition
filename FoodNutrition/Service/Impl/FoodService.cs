using FoodNutrition.Data;
using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Service.Impl
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext dbContext;
        public FoodService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<FoodNutrientResult> CalculateFoodNutrient(FoodNutrientPortion foodNutrientPortion)
        {
            try
            {
                Food food = await dbContext.Foods
                    .Where(f => f.FoodId == foodNutrientPortion.FoodId)
                    .Include(f=>f.Portion.Where(p=>p.PortionId==foodNutrientPortion.PortionId))
                    .Include(f=>f.FoodNutrients)
                    .ThenInclude(f=>f.Nutrient)
                    .SingleAsync();
                return new FoodNutrientResult(foodNutrientPortion,food);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<FoodResult> GetFoodById(int id)
        {
            try
            {
                Food food = await dbContext.Foods
                    .Where(f => f.FoodId == id)
                    .Include(f => f.Category)
                    .Include(f => f.FoodAttributes)
                    .Include(f => f.FoodNutrients)
                    .ThenInclude(f => f.Nutrient)
                    .Include(f=>f.Portion)
                    .SingleAsync();
                return new FoodResult(food);
            }catch(Exception e)
            {
                throw;
            }
        }

        public async Task<List<SearchResult>> SearchByFoodName(string name)
        {
            try
            {
                List<Food> foods = await dbContext.Foods
                    .Where(f => f.Name.ToLower().Contains(name))
                    .Include(f=>f.Category)
                    .Include(f=>f.FoodAttributes)
                    .ToListAsync();
                List<SearchResult> searches=new List<SearchResult>();
                if (foods != null && foods.Any())
                {
                    foreach(Food f in foods)
                    {
                        searches.Add(new SearchResult(f));
                    }
                }
                return searches;
            }catch(Exception e)
            {
                throw;
            }
        }
    }
}
