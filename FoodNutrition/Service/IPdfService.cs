using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Service
{
    public interface IPdfService
    {
        Task<byte[]> CreateFoodNutrientPdf(Food model);
    }
}
