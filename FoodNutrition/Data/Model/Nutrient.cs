using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Model
{
    public class Nutrient
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NutrientId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public FoodNutrient FoodNutrient { get; set; }
        public Nutrient() { }
        public Nutrient(Data.DTO.USDA.Nutrient nutrient)
        {
            NutrientId = nutrient.Id;
            Name = nutrient.Name;
            Unit = nutrient.UnitName;
        }
    }
}
