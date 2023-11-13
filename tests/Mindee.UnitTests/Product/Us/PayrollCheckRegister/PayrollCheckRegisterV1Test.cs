using Mindee.Parsing.Common;
using Mindee.Product.Us.PayrollCheckRegister;

namespace Mindee.UnitTests.Product.Us.PayrollCheckRegister
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
            Assert.Equal(expected: 2, actual: response.Document.Inference.Pages.Count);
            PayrollCheckRegisterV1Document docPrediction = response.Document.Inference.Prediction;
            Assert.Equal(expected: 13, actual: docPrediction.Payments.Count);
            Assert.Equal(expected: "Economists For Hire, LLC", actual: docPrediction.CompanyName.Value);

            // broken output, need to add recursive table support
            // Console.Out.Write(response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            PayrollCheckRegisterV1Document page0Prediction = response.Document.Inference.Pages[0].Prediction;

            Assert.Equal(expected: "Economists For Hire, LLC", actual: page0Prediction.CompanyName.Value);

            Assert.Equal(expected: "1994-12-20", actual: page0Prediction.PeriodStart.Value);
            Assert.Equal(expected: "1994-12-20", actual: page0Prediction.PeriodStart.DateObject?.ToString("yyyy-MM-dd"));
            Assert.Equal(expected: "1994-12-26", actual: page0Prediction.PeriodEnd.Value);
            Assert.Equal(expected: "1994-12-30", actual: page0Prediction.PayDate.Value);

            Assert.Equal(expected: 5, actual: page0Prediction.Payments.Count);

            PayrollCheckRegisterV1Payment payment = page0Prediction.Payments[3];
            Assert.Equal(expected: "Hobbes, Thomas", actual: payment.EmployeeName.Value);

            Assert.Equal(expected: 7, actual: payment.Taxes.Count);
            Assert.Equal(expected: new decimal(0.6000), actual: payment.Taxes[4].Amount);
            Assert.Equal(expected: 0.6, actual: (double?)payment.Taxes[4].Amount);
            Assert.False(payment.Taxes[4].Amount.Equals(other: payment.Taxes[3].Amount));
            Assert.Null(payment.Taxes[4].Base);
            Assert.Equal(expected: "NYSDI", actual: payment.Taxes[4].Code);

            Assert.Single(payment.Earnings);
            Assert.Equal(expected: new decimal(1463.24), actual: payment.Earnings[0].Amount);
            Assert.Equal(expected: new decimal(0), actual: payment.Earnings[0].Hours);
            Assert.Equal(expected: "SA", actual: payment.Earnings[0].Code);

            Assert.Empty(payment.Deductions);
        }

        private static async Task<AsyncPredictResponse<PayrollCheckRegisterV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/us/payroll_check_register/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.DocumentQueueGetAsync<PayrollCheckRegisterV1>(jobId: "fake-job-id");
        }
    }
}
