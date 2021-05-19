using FoodNutrition.Data;
using FoodNutrition.Data.DTO.USDA;
using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Service.Impl
{
    public class USDAService : IUSDAService
    {
        private readonly ApplicationDbContext dbContext;
        public USDAService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task AddAttribute(List<Data.DTO.USDA.Attribute> attributes)
        {
            try
            {
                if (attributes != null && attributes.Any())
                {
                    List<FoodAttribute> attr = new List<FoodAttribute>();
                    foreach(var a in attributes)
                    {
                        if(!dbContext.FoodAttributes.Any(f=>f.FoodAttributeId==a.Id)) attr.Add(new Data.Model.FoodAttribute(a));
                    }
                    await dbContext.AddRangeAsync(attr);
                    await dbContext.SaveChangesAsync();
                }
            }catch(Exception e)
            {
                throw;
            }
        }

        public async Task AddCategory(List<Data.DTO.USDA.Category> categories)
        {
            try
            {
                if (categories != null && categories.Any())
                {
                    List<Data.Model.Category> cate = new List<Data.Model.Category>();
                    foreach(var c in categories)
                    {
                        if(!dbContext.Categories.Any(x=>x.CategoryId==c.Id)) cate.Add(new Data.Model.Category(c));
                    }
                    await dbContext.AddRangeAsync(cate);
                    await dbContext.SaveChangesAsync();
                }
            }catch(Exception e)
            {
                throw;
            }
        }

        public async Task AddFood(List<Data.DTO.USDA.Food> foods)
        {
            try
            {
                if (foods != null && foods.Any())
                {
                    List<Data.Model.Food> food = new List<Data.Model.Food>();
                    foreach (var f in foods)
                    {
                        if(!dbContext.Foods.Any(x=>x.FoodId==f.FdcId))food.Add(new Data.Model.Food(f));
                    }
                    await dbContext.AddRangeAsync(food);
                    await dbContext.SaveChangesAsync();
                }
            }catch(Exception e)
            {
                throw;
            }
        }

        public async Task AddFoodNutrient(List<Data.DTO.USDA.FoodNutrient> foodNutrients)
        {
            try
            {
                if (foodNutrients != null && foodNutrients.Any())
                {
                    List<Data.Model.FoodNutrient> nutrients = new List<Data.Model.FoodNutrient>();
                    foreach (var f in foodNutrients)
                    {
                        int id = int.Parse(f.NutrientId.ToString());
                        if (!dbContext.FoodNutrients.Any(x => x.FoodNutrientId == id)) nutrients.Add(new Data.Model.FoodNutrient(f));
                    }
                    await dbContext.AddRangeAsync(nutrients);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task AddNutrient(List<Data.DTO.USDA.Nutrient> nutrients)
        {
            try
            {
                if (nutrients != null && nutrients.Any())
                {
                    List<Data.Model.Nutrient> nut = new List<Data.Model.Nutrient>();
                    foreach (var n in nutrients)
                    {
                       if(!dbContext.Nutrients.Any(x=>x.NutrientId==n.Id)) nut.Add(new Data.Model.Nutrient(n));
                    }
                    await dbContext.AddRangeAsync(nut);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task AddPortion(List<Data.DTO.USDA.Portion> portions)
        {
            try
            {
                if (portions != null && portions.Any())
                {
                    List<Data.Model.Portion> por = new List<Data.Model.Portion>();
                    foreach (var p in portions)
                    {
                        if(!dbContext.Portions.Any(x=>x.PortionId==p.Id)) por.Add(new Data.Model.Portion(p));
                    }
                    await dbContext.AddRangeAsync(por);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
