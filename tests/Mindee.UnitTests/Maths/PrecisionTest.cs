using System.ComponentModel;
using Mindee.Math;

namespace Mindee.UnitTests.Maths
{
    [Category]
    public class PrecisionTest
    {
        [Fact]
        public void Compare_Double_With_Tolerance_MustSuccess()
        {
            Assert.True(Precision.Equals(0.410, 0.420, 0.011d));
        }

        [Fact]
        public void Compare_Double_With_Tolerance_MustFail()
        {
            Assert.False(Precision.Equals(0.410, 0.420, 0.005d));
        }

        [Fact]
        public void Compare_Double_Without_Tolerance()
        {
            Assert.False(Precision.Equals(0.410, 0.420));
        }

        [Fact]
        public void Compare_Double_With_Tolerance_0()
        {
            Assert.False(Precision.Equals(0.410, 0.420, 0));
        }
    }
}
