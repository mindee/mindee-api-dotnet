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
            Assert.True(FieldConfidence.Certain > FieldConfidence.High);
            Assert.True(FieldConfidence.Certain >= FieldConfidence.High);
            Assert.True(FieldConfidence.High > FieldConfidence.Medium);
            Assert.True(FieldConfidence.High >= FieldConfidence.Medium);
            Assert.True(FieldConfidence.Medium > FieldConfidence.Low);
            Assert.True(FieldConfidence.Medium >= FieldConfidence.Low);

            Assert.True(FieldConfidence.High < FieldConfidence.Certain);
            Assert.True(FieldConfidence.High <= FieldConfidence.Certain);
            Assert.True(FieldConfidence.Medium < FieldConfidence.High);
            Assert.True(FieldConfidence.Medium <= FieldConfidence.High);
            Assert.True(FieldConfidence.Low < FieldConfidence.Medium);
            Assert.True(FieldConfidence.Low <= FieldConfidence.Medium);

            Assert.False(FieldConfidence.Low > FieldConfidence.Certain);
            Assert.False(FieldConfidence.High < FieldConfidence.Medium);
        }
    }
}
