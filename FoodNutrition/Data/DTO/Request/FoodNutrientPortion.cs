using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.Request
{
    public class FoodNutrientPortion
    {
        [JsonPropertyName("foodId")]
        [Required]
        public int FoodId { get; set; }
        [JsonPropertyName("portionId")]
        public int PortionId { get; set; } = 0;
        [JsonPropertyName("amount")]
        public int Amount { get; set; } = 1;
    }
}
