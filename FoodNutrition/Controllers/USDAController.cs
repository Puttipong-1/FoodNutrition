using FoodNutrition.Data.DTO.USDA;
using FoodNutrition.Helper;
using FoodNutrition.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/USDA/")]
    public class USDAController : ControllerBase
    {
        private readonly IUSDAService usdaService;
        public USDAController(IUSDAService _usdaService)
        {
            usdaService = _usdaService;
        }
        [Authorize]
        [HttpPost, Route("addAttribute")]
        public async Task<ActionResult> AddAttributes([FromBody] List<Data.DTO.USDA.Attribute> attributes)
        {
            try
            { 
                await usdaService.AddAttribute(attributes);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpPost,Route("addCategory")]
        public async Task<ActionResult> AddCategories([FromBody]List<Category> categories)
        {
            try
            {
                await usdaService.AddCategory(categories);
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpPost, Route("addFood")]
        public async Task<ActionResult> AddFoods([FromBody] List<Food> foods)
        {
            try
            {
                await usdaService.AddFood(foods);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpPost, Route("addFoodNutrient")]
        public async Task<ActionResult> AddFoodNutrients([FromBody] List<FoodNutrient> foodNutrients)
        {
            try
            {
                await usdaService.AddFoodNutrient(foodNutrients);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpPost, Route("addNutrient")]
        public async Task<ActionResult> AddNutrint([FromBody] List<Nutrient> nutrients)
        {
            try
            {
                await usdaService.AddNutrient(nutrients);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpPost, Route("addPortion")]
        public async Task<ActionResult> AddPortion([FromBody] List<Portion> portions)
        {
            try
            {
                await usdaService.AddPortion(portions);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
