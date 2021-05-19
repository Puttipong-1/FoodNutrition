using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Model
{
    public class Portion
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PortionId { get; set; }
        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int SeqNum { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public double Gram { get; set; }
        public Portion() { }
        public Portion(Data.DTO.USDA.Portion portion)
        {
            PortionId = portion.Id;
            FoodId = portion.FdcId;
            SeqNum = portion.SeqNum;
            Amount = portion.Amount.ToString();
            if (int.TryParse(portion.Modifier.ToString(), out _))Description=portion.PortionDescription;
            else Description = portion.Modifier.ToString();
            Gram = portion.GramWeight;
        }
    }
}
