using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using FoodNutrition.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Controllers
{
    [ApiController]
    [Route("api/category/")]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <response code="200">Found </response>
        /// <response code="400">Not found</response>
        /// <returns></returns>
        [HttpPost,Route("all")]
        public async Task<ActionResult> GetAllCategories()
        {
            try
            {
                List<Category> categories = await categoryService.GetAllCategories();
                return Ok(categories);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Get foods in category
        /// </summary>
        /// <param name="id">Category's id</param>
        /// <response code="200">Found</response>
        /// <response code="400">Not found</response>
        /// <returns></returns>
        [HttpPost,Route("{id:int}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            try
            {
                List<SearchResult> searches = await categoryService.GetFoodsinCategory(id);
                return Ok(searches);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
