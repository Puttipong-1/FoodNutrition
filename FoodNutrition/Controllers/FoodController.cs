using FoodNutrition.Data.DTO.Request;
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
    [Route("api/food/")]
    public class FoodController:ControllerBase
    {
        public readonly IFoodService foodService;
        public FoodController(IFoodService _foodService)
        {
            foodService = _foodService;
        }
        /// <summary>
        /// Search food by name
        /// </summary>
        /// <param name="name">Food's name</param>
        /// <response code="200">Found</response>
        /// <response code="400">Not found</response>
        /// <returns></returns>
        [HttpPost,Route("search/{name}")]
        [ProducesResponseType(typeof(List<SearchResult>), 200)]
        public async Task<ActionResult> SearchFoodByName(string name)
        {
            try
            {
                List<SearchResult> searches = await foodService.SearchByFoodName(name);
                return Ok(searches);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Get food by id
        /// </summary>
        /// <param name="id">Food's id</param>
        /// <response code="200">Found</response>
        /// <response code="400">Not found</response>
        /// <returns></returns>
        [HttpPost, Route("id/{id}")]
        [ProducesResponseType(typeof(FoodResult), 200)]
        [ProducesResponseType(typeof(string),400)]
        public async Task<ActionResult> GetFoodById(int id)
        {
            try
            {
                FoodResult food = await foodService.GetFoodById(id);
                if (food is null) return BadRequest("Food not found");
                return Ok(food);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Calculate nutrient
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Error</response>
        /// <returns></returns>
        [HttpPost, Route("calculate")]
        [ProducesResponseType(typeof(List<FoodNutrientResult>), 200)]
        public async Task<ActionResult> CalculateFoodPortion([FromBody] FoodNutrientPortion fnp)
        {
            try
            {
                FoodNutrientResult result = await foodService.CalculateFoodNutrient(fnp);
                if (result is null) return BadRequest("Food not found");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
