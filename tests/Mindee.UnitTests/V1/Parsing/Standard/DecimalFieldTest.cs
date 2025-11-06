using Mindee.Parsing.Standard;

namespace Mindee.UnitTests.V1.Parsing.Standard
{
    [Trait("Category", "Standard DecimalField")]
    public class DecimalFieldTest
    {
        [Fact]
        public void DecimalField_Must_PrintCorrectly()
        {
            DecimalField field = new DecimalField();
            field.Value = new decimal(0.2);
            Assert.Equal(expected: "0.20", actual: field.ToString());

            field.Value = new decimal(1.2);
            Assert.Equal(expected: "1.20", actual: field.ToString());

            field.Value = new decimal(1.2345);
            Assert.Equal(expected: "1.2345", actual: field.ToString());
        }

        [Fact]
        public void AmountField_Must_PrintCorrectly()
        {
            AmountField field = new AmountField();
            field.Value = 0.2;
            Assert.Equal(expected: "0.20", actual: field.ToString());

            field.Value = 1.2;
            Assert.Equal(expected: "1.20", actual: field.ToString());

            field.Value = 1.2345;
            Assert.Equal(expected: "1.2345", actual: field.ToString());
        }
    }
}
