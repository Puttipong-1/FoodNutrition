using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Pdf
{
    public class FoodNutrientModel
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public string Category { get; set; }
        public string Attribute { get; set; }
        public List<PortionModel> Portions { get; set; }
        public FoodNutrientModel(Food food)
        {
            FoodId = food.FoodId;
            FoodName = food.Name;
            Category = food.Category?.Description;
            if (food.FoodAttributes != null && food.FoodAttributes.Any())
            {
                Attribute = string.Join(",", food.FoodAttributes.Select(x=>x.Value).ToList());
            }
            Portions = new List<PortionModel>();
            Portions.Add(new PortionModel(food.FoodNutrients));
            if(food.Portions != null && food.Portions.Any())
            {
                foreach(Portion p in food.Portions)
                {
                    Portions.Add(new PortionModel(p, food.FoodNutrients));
                }
            }

        }
    }
    public class PortionModel
    {
        public string PortionDesc { get; set; }
        public List<NutrientModel> Nutrients { get; set; }
        public PortionModel(List<FoodNutrient> foodNutrients)
        {
            PortionDesc = "100 g";
            Nutrients = new List<NutrientModel>();
            if (foodNutrients != null && foodNutrients.Any())
            {
                foreach (FoodNutrient a in foodNutrients)
                {
                    Nutrients.Add(new NutrientModel(a));
                }
            }
        }
        public PortionModel(Portion portion, List<FoodNutrient> foodNutrients)
        {
            PortionDesc = $"{portion.Amount} {portion.Description} ({portion.Gram} g)";
            Nutrients = new List<NutrientModel>();
            if (foodNutrients != null && foodNutrients.Any())
            {
                double gram = Math.Round(portion.Gram / 100, 2);
                foreach(FoodNutrient a in foodNutrients)
                {
                    Nutrients.Add(new NutrientModel(a, gram));
                }
            }
        }
    }
    public class NutrientModel {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public NutrientModel(FoodNutrient foodNutrient)
        {
            Name = foodNutrient.Nutrient?.Name;
            Amount = Math.Round(foodNutrient.Amount);
            Unit = foodNutrient.Nutrient?.Unit;
        }
        public NutrientModel(FoodNutrient foodNutrient,double gram)
        {
            Name = foodNutrient.Nutrient?.Name;
            Amount = Math.Round(foodNutrient.Amount * gram, 2);
            Unit = foodNutrient.Nutrient?.Unit;
        }
    }
}
