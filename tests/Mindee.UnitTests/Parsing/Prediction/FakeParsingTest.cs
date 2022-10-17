
using Mindee.Parsing;
using Mindee.Parsing.Common;

namespace Mindee.UnitTests.Parsing.Prediction
{
    public class FakeParsingTest : ParsingTestBase
    {
        [Fact]
        public async Task Execute_WithAnyKindOfData_MustFail()
        {
            var mindeeAPi = GetMindeeApi();

            await Assert.ThrowsAsync<NotSupportedException>(
               () => _ = mindeeAPi.PredictAsync<FakePrediction>(GetFakePredictParameter()));
        }

        private MindeeApi GetMindeeApiForFake()
        {
            return GetMindeeApi("Resources/receipt_response_full_v3.json");
        }
    }

    internal sealed class FakePrediction : PredictionBase
    {

    }
}
