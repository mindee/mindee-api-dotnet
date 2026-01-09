using Mindee.Parsing.Common;
using Mindee.Product.NutritionFactsLabel;

namespace Mindee.UnitTests.V1.Product.NutritionFactsLabel
{
    [Trait("Category", "NutritionFactsLabelV1")]
    public class NutritionFactsLabelV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.ServingPerBox.Value);
            Assert.Null(docPrediction.ServingSize.Amount);
            Assert.Null(docPrediction.ServingSize.Unit);
            Assert.Null(docPrediction.Calories.DailyValue);
            Assert.Null(docPrediction.Calories.Per100G);
            Assert.Null(docPrediction.Calories.PerServing);
            Assert.Null(docPrediction.TotalFat.DailyValue);
            Assert.Null(docPrediction.TotalFat.Per100G);
            Assert.Null(docPrediction.TotalFat.PerServing);
            Assert.Null(docPrediction.SaturatedFat.DailyValue);
            Assert.Null(docPrediction.SaturatedFat.Per100G);
            Assert.Null(docPrediction.SaturatedFat.PerServing);
            Assert.Null(docPrediction.TransFat.DailyValue);
            Assert.Null(docPrediction.TransFat.Per100G);
            Assert.Null(docPrediction.TransFat.PerServing);
            Assert.Null(docPrediction.Cholesterol.DailyValue);
            Assert.Null(docPrediction.Cholesterol.Per100G);
            Assert.Null(docPrediction.Cholesterol.PerServing);
            Assert.Null(docPrediction.TotalCarbohydrate.DailyValue);
            Assert.Null(docPrediction.TotalCarbohydrate.Per100G);
            Assert.Null(docPrediction.TotalCarbohydrate.PerServing);
            Assert.Null(docPrediction.DietaryFiber.DailyValue);
            Assert.Null(docPrediction.DietaryFiber.Per100G);
            Assert.Null(docPrediction.DietaryFiber.PerServing);
            Assert.Null(docPrediction.TotalSugars.DailyValue);
            Assert.Null(docPrediction.TotalSugars.Per100G);
            Assert.Null(docPrediction.TotalSugars.PerServing);
            Assert.Null(docPrediction.AddedSugars.DailyValue);
            Assert.Null(docPrediction.AddedSugars.Per100G);
            Assert.Null(docPrediction.AddedSugars.PerServing);
            Assert.Null(docPrediction.Protein.DailyValue);
            Assert.Null(docPrediction.Protein.Per100G);
            Assert.Null(docPrediction.Protein.PerServing);
            Assert.Null(docPrediction.Sodium.DailyValue);
            Assert.Null(docPrediction.Sodium.Per100G);
            Assert.Null(docPrediction.Sodium.PerServing);
            Assert.Null(docPrediction.Sodium.Unit);
            Assert.Empty(docPrediction.Nutrients);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "nutrition_facts/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<NutritionFactsLabelV1>> GetPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/nutrition_facts/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<NutritionFactsLabelV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
