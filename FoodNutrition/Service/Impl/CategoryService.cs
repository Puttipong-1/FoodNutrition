using FoodNutrition.Data;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using FoodNutrition.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Service.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await categoryRepository.GetAllCategory();
        }

        public async Task<List<SearchResult>> GetFoodsinCategory(int code)
        {
            Category category = await categoryRepository.GetCategoryByCode(code);
            if (category == null) return null;
            List<SearchResult> searches = new List<SearchResult>();
            if (category.Foods != null && category.Foods.Any())
            {
                foreach (Food f in category.Foods)
                {
                    searches.Add(new SearchResult(f, category));
                }
                return searches;
            }
            return searches;
        }
    }
}
