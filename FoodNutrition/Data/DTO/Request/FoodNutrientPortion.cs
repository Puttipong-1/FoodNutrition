using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.Request
{
    public class FoodNutrientPortion
    {
        [JsonPropertyName("foodId")]
        public int FoodId { get; set; }
        [JsonPropertyName("portionId")]
        public int PortionId { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; } = 1;
    }
}
