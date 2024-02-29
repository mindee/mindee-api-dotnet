using Mindee.Parsing.Common;
using Mindee.Product.Fr.CarteGrise;

namespace Mindee.UnitTests.Product.Fr.CarteGrise
{
    [Trait("Category", "CarteGriseV1")]
    public class CarteGriseV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.A.Value);
            Assert.Null(docPrediction.B.Value);
            Assert.Null(docPrediction.C1.Value);
            Assert.Null(docPrediction.C3.Value);
            Assert.Null(docPrediction.C41.Value);
            Assert.Null(docPrediction.C4A.Value);
            Assert.Null(docPrediction.D1.Value);
            Assert.Null(docPrediction.D3.Value);
            Assert.Null(docPrediction.E.Value);
            Assert.Null(docPrediction.F1.Value);
            Assert.Null(docPrediction.F2.Value);
            Assert.Null(docPrediction.F3.Value);
            Assert.Null(docPrediction.G.Value);
            Assert.Null(docPrediction.G1.Value);
            Assert.Null(docPrediction.I.Value);
            Assert.Null(docPrediction.J.Value);
            Assert.Null(docPrediction.J1.Value);
            Assert.Null(docPrediction.J2.Value);
            Assert.Null(docPrediction.J3.Value);
            Assert.Null(docPrediction.P1.Value);
            Assert.Null(docPrediction.P2.Value);
            Assert.Null(docPrediction.P3.Value);
            Assert.Null(docPrediction.P6.Value);
            Assert.Null(docPrediction.Q.Value);
            Assert.Null(docPrediction.S1.Value);
            Assert.Null(docPrediction.S2.Value);
            Assert.Null(docPrediction.U1.Value);
            Assert.Null(docPrediction.U2.Value);
            Assert.Null(docPrediction.V7.Value);
            Assert.Null(docPrediction.X1.Value);
            Assert.Null(docPrediction.Y1.Value);
            Assert.Null(docPrediction.Y2.Value);
            Assert.Null(docPrediction.Y3.Value);
            Assert.Null(docPrediction.Y4.Value);
            Assert.Null(docPrediction.Y5.Value);
            Assert.Null(docPrediction.Y6.Value);
            Assert.Null(docPrediction.FormulaNumber.Value);
            Assert.Null(docPrediction.OwnerFirstName.Value);
            Assert.Null(docPrediction.OwnerSurname.Value);
            Assert.Null(docPrediction.Mrz1.Value);
            Assert.Null(docPrediction.Mrz2.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/carte_grise/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<CarteGriseV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/carte_grise/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<CarteGriseV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
