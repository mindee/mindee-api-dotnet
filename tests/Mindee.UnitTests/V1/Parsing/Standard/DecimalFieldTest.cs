using Mindee.Parsing.Standard;

namespace Mindee.UnitTests.V1.Parsing.Standard
{
    [Trait("Category", "Standard DecimalField")]
    public class DecimalFieldTest
    {
        [Fact]
        public void DecimalField_Must_PrintCorrectly()
        {
            var field = new DecimalField();
            field.Value = new decimal(0.2);
            Assert.Equal("0.20", field.ToString());

            field.Value = new decimal(1.2);
            Assert.Equal("1.20", field.ToString());

            field.Value = new decimal(1.2345);
            Assert.Equal("1.2345", field.ToString());
        }

        [Fact]
        public void AmountField_Must_PrintCorrectly()
        {
            var field = new AmountField();
            field.Value = 0.2;
            Assert.Equal("0.20", field.ToString());

            field.Value = 1.2;
            Assert.Equal("1.20", field.ToString());

            field.Value = 1.2345;
            Assert.Equal("1.2345", field.ToString());
        }
    }
}
