using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Model
{
    public class FoodNutrient
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FoodNutrientId { get; set; }
        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public Food Food { get; set; }
        [ForeignKey("Nutrient")]
        public int NutrientId { get; set; }
        public Nutrient Nutrient { get; set; }
        public double Amount { get; set; }
        public FoodNutrient() { }
        public FoodNutrient(Data.DTO.USDA.FoodNutrient nutrient)
        {
            FoodNutrientId = int.Parse(nutrient.Id.ToString());
            FoodId = int.Parse(nutrient.FdcId.ToString());
            NutrientId = int.Parse(nutrient.NutrientId.ToString());
            Amount = double.Parse(nutrient.Amount.ToString());
        }
    }
}
