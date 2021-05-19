using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.Response
{
    public class Nutrients
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public Nutrients() { }
        public Nutrients(FoodNutrient food)
        {
            Name = food.Nutrient.Name;
            Amount = food.Amount;
            Unit = food.Nutrient.Unit;
        } 
        public Nutrients(double gram,FoodNutrient food)
        {
            Name = food.Nutrient.Name;
            Amount = Math.Round(gram * food.Amount, 2);
            Unit = food.Nutrient.Unit;
        }
    }
}
