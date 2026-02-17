namespace Mindee.IntegrationTests
{
    internal sealed class IntegrationFactAttribute : FactAttribute
    {
        public IntegrationFactAttribute() => Timeout = 180000;
    }

    internal sealed class IntegrationTheoryAttribute : TheoryAttribute
    {
        public IntegrationTheoryAttribute() => Timeout = 180000;
    }
}
