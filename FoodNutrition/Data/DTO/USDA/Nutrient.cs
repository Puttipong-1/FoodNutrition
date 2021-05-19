using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.USDA
{
    public class Nutrient
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("unit_name")]
        public string UnitName { get; set; }

        [JsonPropertyName("nutrient_nbr")]
        public double? NutrientNbr { get; set; }

        [JsonPropertyName("rank")]
        public object Rank { get; set; }
    }
}
