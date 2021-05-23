using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.Response
{
    public class FoodPortion
    {
        public string FoodId { get; set; }
        public string Name { get; set; }
        public List<Portions> Portions { get; set; }
    }
}
