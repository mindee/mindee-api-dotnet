using Mindee.Parsing.Common;
using Mindee.Product.Us.PayrollCheckRegister;

namespace Mindee.UnitTests.V1.Product.Us.PayrollCheckRegister
{
    [Trait("Category", "PayrollCheckRegisterV1")]
    public class PayrollCheckRegisterV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            Assert.Null(response.Document.Inference.Prediction.CompanyName.Value);
            Assert.Null(response.Document.Inference.Prediction.PeriodStart.Value);
            Assert.Null(response.Document.Inference.Prediction.PeriodEnd.Value);
            Assert.Null(response.Document.Inference.Prediction.PayDate.Value);
            Assert.Empty(response.Document.Inference.Prediction.Payments);
        }

        [Fact]
        public async Task Predict_CheckDocument()
        {
            var response = await GetPrediction("complete");
            Assert.Equal(2, response.Document.Inference.Pages.Count);
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Equal(13, docPrediction.Payments.Count);
            Assert.Equal("Economists For Hire, LLC", docPrediction.CompanyName.Value);

            // broken output, need to add recursive table support
            // Console.Out.Write(response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var page0Prediction = response.Document.Inference.Pages[0].Prediction;

            Assert.Equal("Economists For Hire, LLC", page0Prediction.CompanyName.Value);

            Assert.Equal("1994-12-20", page0Prediction.PeriodStart.Value);
            Assert.Equal("1994-12-20", page0Prediction.PeriodStart.DateObject?.ToString("yyyy-MM-dd"));
            Assert.Equal("1994-12-26", page0Prediction.PeriodEnd.Value);
            Assert.Equal("1994-12-30", page0Prediction.PayDate.Value);

            Assert.Equal(5, page0Prediction.Payments.Count);

            var payment = page0Prediction.Payments[3];
            Assert.Equal("Hobbes, Thomas", payment.EmployeeName.Value);

            Assert.Equal(7, payment.Taxes.Count);
            Assert.Equal(new decimal(0.6000), payment.Taxes[4].Amount);
            Assert.Equal(0.6, (double?)payment.Taxes[4].Amount);
            Assert.False(payment.Taxes[4].Amount.Equals(payment.Taxes[3].Amount));
            Assert.Null(payment.Taxes[4].Base);
            Assert.Equal("NYSDI", payment.Taxes[4].Code);

            Assert.Single(payment.Earnings);
            Assert.Equal(new decimal(1463.24), payment.Earnings[0].Amount);
            Assert.Equal(new decimal(0), payment.Earnings[0].Hours);
            Assert.Equal("SA", payment.Earnings[0].Code);

            Assert.Empty(payment.Deductions);
        }

        private static async Task<AsyncPredictResponse<PayrollCheckRegisterV1>> GetPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/payroll_check_register/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.DocumentQueueGetAsync<PayrollCheckRegisterV1>("fake-job-id");
        }
    }
}
