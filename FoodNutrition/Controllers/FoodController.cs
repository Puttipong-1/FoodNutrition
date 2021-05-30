using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using FoodNutrition.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Controllers
{
    [ApiController]
    [Route("api/food/")]
    public class FoodController:ControllerBase
    {
        private readonly IFoodService foodService;
        private readonly IPdfService pdfService;
        public FoodController(IFoodService _foodService,IPdfService _pdfService)
        {
            foodService = _foodService;
            pdfService = _pdfService;
        }
        /// <summary>
        /// Search food by name
        /// </summary>
        /// <param name="name">Food's name</param>
        /// <response code="200">Found foods</response>
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
        /// <response code="200">Found food</response>
        /// <response code="400">Not found</response>
        /// <returns></returns>
        [HttpPost, Route("id/{id}")]
        [ProducesResponseType(typeof(FoodResult), 200)]
        [ProducesResponseType(typeof(string),400)]
        public async Task<ActionResult> GetFoodById(int id)
        {
            try
            {
                Food food = await foodService.GetFoodById(id);
                if (food is null) return BadRequest("Food not found");
                return Ok(new FoodResult(food));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Calculate nutrient
        /// </summary>
        /// <response code="200">Calculate nutreint success</response>
        /// <response code="400">Food not found</response>
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
        /// <summary>
        /// Generate food nutrient pdf.
        /// </summary>
        /// <param name="id">Food's id</param>
        /// <response code="200">Generate pdf success</response>
        /// <response code="400">Fail to generate pdf</response>
        /// <returns></returns>
        [HttpPost, Route("pdf/{id}")]
        [Produces("application/pdf")]
        public async Task<ActionResult> GetFoodNutrientPdf(int id)
        {
            try
            {
                Food food = await foodService.GetFoodById(id);
                if (food is null) return BadRequest("Food not found");
                byte[] pdfByte = await pdfService.CreateFoodNutrientPdf(food);
                MemoryStream ms = new MemoryStream(pdfByte);
                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
