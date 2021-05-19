using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Model
{
    public class Food
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FoodId { get; set; }
        public string Name { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public List<FoodAttribute> FoodAttributes { get; set; }
        public List<FoodNutrient> FoodNutrients { get; set; }
        public List<Portion> Portion { get; set; }
        public Food() { }
        public Food(Data.DTO.USDA.Food food)
        {
            FoodId = food.FdcId;
            Name = food.Description;
            CategoryId = food.FoodCategoryId;
        }
    }
}
