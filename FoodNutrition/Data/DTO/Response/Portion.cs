using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.Response
{
    public class Portion
    {
        public int PortionId { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public double Gram { get; set; }
        public Portion() { }
        public Portion(Model.Portion p)
        {
            PortionId = p.PortionId;
            Amount = p.Amount;
            Description = p.Description;
            Gram = p.Gram;
        }
    }

}
