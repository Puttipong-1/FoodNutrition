using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.Response
{
    public class FoodNutrientResult
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Portion { get; set; }
        public int Amount { get; set; }
        public List<Nutrients> Nutrients { get; set; }
        public FoodNutrientResult() { }
        public FoodNutrientResult(FoodNutrientPortion fnp,Food food) {
            FoodId = food.FoodId;
            Name = food.Name;
            double gram = 1.0;
            Amount = fnp.Amount;
            Nutrients = new List<Nutrients>();
            if(food.Portions is null||food.Portions.Any()==false)
            {
                Portion = "100 g";
                gram = gram * Amount;
            }
            else
            {
                var p = food.Portions[0];
                gram = (p.Gram*Amount) / 100;
                Portion = $"{p.Amount} {p.Description} ({p.Gram})";
            }
            if (food.FoodNutrients != null && food.FoodNutrients.Any())
            {
                foreach (var n in food.FoodNutrients)
                {
                    if (n != null) Nutrients.Add(new Nutrients(gram, n));
                }
            }
        }
    }
}
