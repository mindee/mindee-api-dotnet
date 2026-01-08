using Mindee.Input;
using Mindee.Parsing.V2;

namespace Mindee.UnitTests.V2.Parsing
{
    [Trait("Category", "V2")]
    [Trait("Category", "Job")]
    public class JobTest
    {
        [Fact]
        public void OkProcessing_MustHaveValidProperties()
        {
            var response = GetJob("job/ok_processing.json");
            Assert.NotNull(response.Job);
            Assert.NotNull(response.Job.Id);
            Assert.Equal(2025, response.Job.CreatedAt.Year);
            Assert.StartsWith("https", response.Job.PollingUrl);
            Assert.Null(response.Job.ResultUrl);
            Assert.Null(response.Job.Error);
        }

        [Fact]
        public void OkProcessed_WebhooksOk_MustHaveValidProperties()
        {
            var response = GetJob("job/ok_processed_webhooks_ok.json");
            Assert.NotNull(response.Job);
            Assert.NotNull(response.Job.Id);
            Assert.Equal(2026, response.Job.CreatedAt.Year);
            Assert.StartsWith("https", response.Job.PollingUrl);
            Assert.StartsWith("https", response.Job.ResultUrl);
            Assert.Null(response.Job.Error);
            Assert.NotEmpty(response.Job.Webhooks);
            var webhook = response.Job.Webhooks.First();
            Assert.NotNull(webhook.Id);
            Assert.Equal(2026, webhook.CreatedAt.Year);
            Assert.Equal("Processed", webhook.Status);
            Assert.Null(webhook.Error);
        }

        [Fact]
        public void Error_422_MustHaveValidProperties()
        {
            var response = GetJob("job/fail_422.json");
            Assert.NotNull(response.Job);
            Assert.NotNull(response.Job.Id);
            Assert.Equal(2025, response.Job.CreatedAt.Year);
            var error = response.Job.Error;
            Assert.NotNull(error);
            Assert.Equal(422, error.Status);
            Assert.StartsWith("422-", error.Code);
            Assert.Single(error.Errors);
            Assert.Contains("must be a valid", error.Errors.First().Detail);
        }

        private static JobResponse GetJob(string path)
        {
            var localResponse = new LocalResponse(
                File.ReadAllText(Constants.V2RootDir + path));
            return localResponse.DeserializeResponse<JobResponse>();
        }
    }
}
