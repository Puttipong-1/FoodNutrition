using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.USDA
{
    public class FoodNutrient
    {
        [JsonPropertyName("id")]
        public object Id { get; set; }

        [JsonPropertyName("fdc_id")]
        public object FdcId { get; set; }

        [JsonPropertyName("nutrient_id")]
        public object NutrientId { get; set; }

        [JsonPropertyName("amount")]
        public object Amount { get; set; }

        [JsonPropertyName("data_points")]
        public object DataPoints { get; set; }

        [JsonPropertyName("derivation_id")]
        public object DerivationId { get; set; }

        [JsonPropertyName("min")]
        public object Min { get; set; }

        [JsonPropertyName("max")]
        public object Max { get; set; }

        [JsonPropertyName("median")]
        public object Median { get; set; }

        [JsonPropertyName("footnote")]
        public object Footnote { get; set; }

        [JsonPropertyName("min_year_acquired")]
        public object MinYearAcquired { get; set; }
    }
}
