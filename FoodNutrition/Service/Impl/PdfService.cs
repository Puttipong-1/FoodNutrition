using FoodNutrition.Data.Model;
using FoodNutrition.Data.Pdf;
using FoodNutrition.Helper;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FoodNutrition.Service.Impl
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> CreateFoodNutrientPdf(Food food)
        {
            return await Task.Run(() =>
            {
                var model = new FoodNutrientModel(food);
                var document = new FoodNutreintDocument(model);
                return document.GeneratePdf();
            });
        }
    }
}
