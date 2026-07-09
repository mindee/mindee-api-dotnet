using Mindee.V2.Parsing.Inference.Field;

namespace Mindee.UnitTests.V2.Parsing
{
    [Trait("Category", "V2")]
    [Trait("Category", "Parsing")]
    public class FieldConfidenceExtensionsTest
    {
        [Fact]
        public void ComparisonHelpers_MustReturnExpectedOrdering()
        {
            Assert.True(FieldConfidence.Certain.GreaterThan(FieldConfidence.High));
            Assert.True(FieldConfidence.High.GreaterThanOrEqual(FieldConfidence.High));
            Assert.True(FieldConfidence.Low.LessThan(FieldConfidence.Medium));
            Assert.True(FieldConfidence.Medium.LessThanOrEqual(FieldConfidence.Certain));

            Assert.False(FieldConfidence.Low.GreaterThan(FieldConfidence.Certain));
            Assert.False(FieldConfidence.High.LessThan(FieldConfidence.Medium));
        }
    }
}
