using FoodNutrition.Data.Pdf;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Helper
{
    public class FoodNutreintDocument:IDocument
    {
        public FoodNutrientModel Model { get; }
        private int Index { get; set; }
        public FoodNutreintDocument(FoodNutrientModel model)
        {
            Model = model;
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IContainer container)
        {
            container.PaddingVertical(50)
                .PaddingHorizontal(50)
                .Page(page =>
                {
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().AlignCenter().PageNumber("Page {number}");
                });
        }
        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeColumn().Stack(stack =>
                {
                    stack.Item().Text($"Name: {Model.FoodName}");
                    stack.Item().Text($"Category: {Model.Category}");
                    stack.Item().Text($"Attribute: {Model.Attribute}");
                });
                row.ConstantColumn(100).Height(50).Placeholder();
            });
        }
        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Stack(column =>
            {
                if (Model.Portions.Count > 0)
                {
                    Index = 0;
                    foreach(PortionModel p in Model.Portions)
                    {
                        column.Item().Row(row =>
                        { 
                            row.RelativeColumn().AlignCenter().Text($"{p.PortionDesc}",TextStyle.Default.Size(15).Bold());
                        });
                        column.Item().Element(ComposeTable);
                        Index++;
                    }
                }
            });
        }
        private void ComposeTable(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold();
            container.PaddingTop(20).PaddingBottom(20).Decoration(decoration =>
            {
                decoration.Header().BorderBottom(1).Padding(5).Row(row =>
                {
                    row.ConstantColumn(25).Text("#", headerStyle);
                    row.RelativeColumn(3).Text("Nutrient",headerStyle);
                    row.RelativeColumn().AlignRight().Text("Value", headerStyle);
                    row.RelativeColumn().AlignRight().Text("Unit", headerStyle);
                });
                decoration.Content()
                    .Stack(column =>
                    {
                        if (Model.Portions[Index].Nutrients.Count > 0)
                        {
                            int i = 1;
                            foreach(var item in Model.Portions[Index].Nutrients)
                            {
                                column.Item().BorderBottom(1).BorderColor("CCC").Padding(5).Row(row =>
                                {
                                    row.ConstantColumn(25).Text(i);
                                    row.RelativeColumn(3).Text(item.Name);
                                    row.RelativeColumn().AlignRight().Text(item.Amount);
                                    row.RelativeColumn().AlignRight().Text(item.Unit);
                                });
                                i++;
                            }
                        }
                    });
            });

        }
    }
}
