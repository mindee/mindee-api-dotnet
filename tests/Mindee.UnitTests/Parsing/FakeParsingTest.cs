using Mindee.Parsing;
using Mindee.Parsing.Common;

namespace Mindee.UnitTests.Parsing
{
    public class FakeParsingTest
    {
        [Fact]
        [Trait("Category", "Wrong prediction configuration")]
        public async Task Execute_WithAnyKindOfData_MustFail()
        {
            var mindeeAPi = GetMindeeApiForFake();

            await Assert.ThrowsAsync<NotSupportedException>(
               () => _ = mindeeAPi.PredictAsync<FakePrediction>(ParsingTestBase.GetFakePredictParameter()));
        }

        private MindeeApi GetMindeeApiForFake()
        {
            return ParsingTestBase.GetMindeeApi("Resources/invoice/response_v3/complete.json");
        }
    }

    internal sealed class FakePrediction : PredictionBase
    {

    }
}
