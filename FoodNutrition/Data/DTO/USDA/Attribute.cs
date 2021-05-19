using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.USDA
{
    public class Attribute
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fdc_id")]
        public int FdcId { get; set; }

        [JsonPropertyName("seq_num")]
        public int SeqNum { get; set; } = 0;

        [JsonPropertyName("food_attribute_type_id")]
        public int FoodAttributeTypeId { get; set; }

        [JsonPropertyName("name")]
        public object Name { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }
    }
}
