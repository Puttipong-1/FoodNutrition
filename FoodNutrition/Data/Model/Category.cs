using FoodNutrition.Data.DTO.USDA;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Model
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public List<Food> Foods { get; set; }
        public Category() { }
        public Category(Data.DTO.USDA.Category category)
        {
            CategoryId = category.Id;
            Code = category.Code;
            Description = category.Description;
        }
    }
}
