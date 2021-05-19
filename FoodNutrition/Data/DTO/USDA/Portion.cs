using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.USDA
{
    public class Portion
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fdc_id")]
        public int FdcId { get; set; }

        [JsonPropertyName("seq_num")]
        public int SeqNum { get; set; }

        [JsonPropertyName("amount")]
        public object Amount { get; set; }

        [JsonPropertyName("measure_unit_id")]
        public int MeasureUnitId { get; set; }

        [JsonPropertyName("portion_description")]
        public string PortionDescription { get; set; }

        [JsonPropertyName("modifier")]
        public object Modifier { get; set; }

        [JsonPropertyName("gram_weight")]
        public double GramWeight { get; set; }

        [JsonPropertyName("data_points")]
        public object DataPoints { get; set; }

        [JsonPropertyName("footnote")]
        public object Footnote { get; set; }

        [JsonPropertyName("min_year_acquired")]
        public object MinYearAcquired { get; set; }
    }
}
