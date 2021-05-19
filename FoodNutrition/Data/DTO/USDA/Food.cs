using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.USDA
{
    public class Food
    {
        [JsonPropertyName("fdc_id")]
        public int FdcId { get; set; }

        [JsonPropertyName("data_type")]
        public string DataType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("food_category_id")]
        public int? FoodCategoryId { get; set; }

        [JsonPropertyName("publication_date")]
        public string PublicationDate { get; set; }
    }
}
