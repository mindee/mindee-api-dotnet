using System;
using System.Runtime.CompilerServices;

namespace Mindee.Tests
{
    internal static class TestTimeouts
    {
#pragma warning disable CA2255
        [ModuleInitializer]
#pragma warning restore CA2255
        internal static void Init()
        {
            Environment.SetEnvironmentVariable("MINDEE_TEST_HARD_TIMEOUT_SECONDS", "60");
        }
    }
}

#if NET472 || NET48
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class ModuleInitializerAttribute : Attribute
    {
    }
}
#endif
