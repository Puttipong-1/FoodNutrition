using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.Response
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AdditionDescription { get; set; }
        public string Category { get; set; }
        public int CategoryCode { get; set; }
        public SearchResult(){}
        public SearchResult(Food food) {
            Id = food.FoodId;
            Name = food.Name;
            AdditionDescription = string.Empty;
           /* if (food.FoodAttributes != null && food.FoodAttributes.Any())
            {
                foreach (var a in food.FoodAttributes)
                {
                    if (!int.TryParse(a.Value, out _))
                    {
                        AdditionDescription += a.Value + ",";
                    }
                }
                AdditionDescription = AdditionDescription.Substring(0, AdditionDescription.Length - 1);
            }
            if (food.Category != null)
            {
                Category = food.Category.Description;
                CategoryCode = food.Category.Code;
            }*/
        }
        public SearchResult(Food food,Category category)
        {
            Id = food.FoodId;
            Name = food.Name;
            AdditionDescription = string.Empty;
            if (food.FoodAttributes != null&&food.FoodAttributes.Any())
            {
                foreach(var a in food.FoodAttributes)
                {
                    if(!int.TryParse(a.Value,out _))
                    {
                        AdditionDescription += a.Value+",";
                    }
                }
                AdditionDescription = AdditionDescription.Substring(0, AdditionDescription.Length - 1);
            }
            Category = category.Description;
            CategoryCode = category.Code;
        }
    }
}
