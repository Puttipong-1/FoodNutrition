﻿using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.Response
{
    public class FoodResult
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Attribute { get; set; }
        public List<Nutrients> Nutrients { get; set; }
        public List<Portions> Portions { get; set; }
        public FoodResult() { }
        public FoodResult(Food food) {
            FoodId = food.FoodId;
            Name = food.Name;
            Category = food.Category == null ? "" : food.Category.Description;
            if (food.FoodAttributes != null && food.FoodAttributes.Any())
            {
               Attribute=string.Join(",", food.FoodAttributes.Select(x => x.Value));
            }
            Nutrients = new List<Nutrients>();
            if (food.FoodNutrients != null && food.FoodNutrients.Any())
            {
                foreach(var f in food.FoodNutrients)
                {
                    Nutrients.Add(new Nutrients(f));
                }
            }
            Portions = new List<Portions>();
            if (food.Portions != null && food.Portions.Any())
            {
                foreach(var p in food.Portions)
                {
                    Portions.Add(new Portions(p));
                }
            }
        
        }
    }
}
