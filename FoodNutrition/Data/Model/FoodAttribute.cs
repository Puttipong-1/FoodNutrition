using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Model
{
    public class FoodAttribute
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FoodAttributeId { get; set; }
        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int SeqNum { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public FoodAttribute(){ }
        public FoodAttribute(Data.DTO.USDA.Attribute attribute)
        {
            FoodAttributeId = attribute.Id;
            FoodId = attribute.FdcId;
            Name = attribute.Name.ToString();
            Value = attribute.Value.ToString();
            SeqNum = attribute.SeqNum;
        }
        
    }
}
