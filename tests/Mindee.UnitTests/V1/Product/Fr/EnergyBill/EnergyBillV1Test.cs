using Mindee.Parsing.Common;
using Mindee.Product.Fr.EnergyBill;

namespace Mindee.UnitTests.V1.Product.Fr.EnergyBill
{
    [Trait("Category", "EnergyBillV1")]
    public class EnergyBillV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.InvoiceNumber.Value);
            Assert.Null(docPrediction.ContractId.Value);
            Assert.Null(docPrediction.DeliveryPoint.Value);
            Assert.Null(docPrediction.InvoiceDate.Value);
            Assert.Null(docPrediction.DueDate.Value);
            Assert.Null(docPrediction.TotalBeforeTaxes.Value);
            Assert.Null(docPrediction.TotalTaxes.Value);
            Assert.Null(docPrediction.TotalAmount.Value);
            Assert.Null(docPrediction.EnergySupplier.Address);
            Assert.Null(docPrediction.EnergySupplier.Name);
            Assert.Null(docPrediction.EnergyConsumer.Address);
            Assert.Null(docPrediction.EnergyConsumer.Name);
            Assert.Empty(docPrediction.Subscription);
            Assert.Empty(docPrediction.EnergyUsage);
            Assert.Empty(docPrediction.TaxesAndContributions);
            Assert.Null(docPrediction.MeterDetails.MeterNumber);
            Assert.Null(docPrediction.MeterDetails.MeterType);
            Assert.Null(docPrediction.MeterDetails.Unit);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "energy_bill_fra/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<EnergyBillV1>> GetPrediction(string name)
        {
            string fileName = Constants.V1RootDir + $"products/energy_bill_fra/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<EnergyBillV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
