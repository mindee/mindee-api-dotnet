using Mindee.Parsing.Common;
using Mindee.Product.Fr.Payslip;

namespace Mindee.UnitTests.V1.Product.Fr.Payslip
{
    [Trait("Category", "PayslipV3")]
    public class PayslipV3Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.PayPeriod.EndDate);
            Assert.Null(docPrediction.PayPeriod.Month);
            Assert.Null(docPrediction.PayPeriod.PaymentDate);
            Assert.Null(docPrediction.PayPeriod.StartDate);
            Assert.Null(docPrediction.PayPeriod.Year);
            Assert.Null(docPrediction.Employee.Address);
            Assert.Null(docPrediction.Employee.DateOfBirth);
            Assert.Null(docPrediction.Employee.FirstName);
            Assert.Null(docPrediction.Employee.LastName);
            Assert.Null(docPrediction.Employee.PhoneNumber);
            Assert.Null(docPrediction.Employee.RegistrationNumber);
            Assert.Null(docPrediction.Employee.SocialSecurityNumber);
            Assert.Null(docPrediction.Employer.Address);
            Assert.Null(docPrediction.Employer.CompanyId);
            Assert.Null(docPrediction.Employer.CompanySite);
            Assert.Null(docPrediction.Employer.NafCode);
            Assert.Null(docPrediction.Employer.Name);
            Assert.Null(docPrediction.Employer.PhoneNumber);
            Assert.Null(docPrediction.Employer.UrssafNumber);
            Assert.Null(docPrediction.BankAccountDetails.BankName);
            Assert.Null(docPrediction.BankAccountDetails.Iban);
            Assert.Null(docPrediction.BankAccountDetails.Swift);
            Assert.Null(docPrediction.Employment.Category);
            Assert.Null(docPrediction.Employment.Coefficient);
            Assert.Null(docPrediction.Employment.CollectiveAgreement);
            Assert.Null(docPrediction.Employment.JobTitle);
            Assert.Null(docPrediction.Employment.PositionLevel);
            Assert.Null(docPrediction.Employment.SeniorityDate);
            Assert.Null(docPrediction.Employment.StartDate);
            Assert.Empty(docPrediction.SalaryDetails);
            Assert.Null(docPrediction.PayDetail.GrossSalary);
            Assert.Null(docPrediction.PayDetail.GrossSalaryYtd);
            Assert.Null(docPrediction.PayDetail.IncomeTaxRate);
            Assert.Null(docPrediction.PayDetail.IncomeTaxWithheld);
            Assert.Null(docPrediction.PayDetail.NetPaid);
            Assert.Null(docPrediction.PayDetail.NetPaidBeforeTax);
            Assert.Null(docPrediction.PayDetail.NetTaxable);
            Assert.Null(docPrediction.PayDetail.NetTaxableYtd);
            Assert.Null(docPrediction.PayDetail.TotalCostEmployer);
            Assert.Null(docPrediction.PayDetail.TotalTaxesAndDeductions);
            Assert.Empty(docPrediction.PaidTimeOff);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "payslip_fra/response_v3/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<PayslipV3>> GetPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/payslip_fra/response_v3/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<PayslipV3>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
