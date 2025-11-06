using Mindee.Parsing.Common;
using Mindee.Product.BillOfLading;

namespace Mindee.UnitTests.V1.Product.BillOfLading
{
    [Trait("Category", "BillOfLadingV1")]
    public class BillOfLadingV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.BillOfLadingNumber.Value);
            Assert.Null(docPrediction.Shipper.Address);
            Assert.Null(docPrediction.Shipper.Email);
            Assert.Null(docPrediction.Shipper.Name);
            Assert.Null(docPrediction.Shipper.Phone);
            Assert.Null(docPrediction.Consignee.Address);
            Assert.Null(docPrediction.Consignee.Email);
            Assert.Null(docPrediction.Consignee.Name);
            Assert.Null(docPrediction.Consignee.Phone);
            Assert.Null(docPrediction.NotifyParty.Address);
            Assert.Null(docPrediction.NotifyParty.Email);
            Assert.Null(docPrediction.NotifyParty.Name);
            Assert.Null(docPrediction.NotifyParty.Phone);
            Assert.Null(docPrediction.Carrier.Name);
            Assert.Null(docPrediction.Carrier.ProfessionalNumber);
            Assert.Null(docPrediction.Carrier.Scac);
            Assert.Empty(docPrediction.CarrierItems);
            Assert.Null(docPrediction.PortOfLoading.Value);
            Assert.Null(docPrediction.PortOfDischarge.Value);
            Assert.Null(docPrediction.PlaceOfDelivery.Value);
            Assert.Null(docPrediction.DateOfIssue.Value);
            Assert.Null(docPrediction.DepartureDate.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "bill_of_lading/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<BillOfLadingV1>> GetPrediction(string name)
        {
            string fileName = Constants.V1RootDir + $"products/bill_of_lading/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<BillOfLadingV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
