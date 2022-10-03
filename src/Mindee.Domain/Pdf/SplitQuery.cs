using System.IO;

namespace Mindee.Domain.Pdf
{
    public sealed class SplitQuery
    {
        public Stream Stream { get; }
        public ushort PageNumberStart { get; }
        public ushort PageNumberEnd { get; }

        public SplitQuery(Stream stream, ushort pageNumberStart, ushort pageNumberEnd)
        {
            Stream = stream;
            PageNumberStart = pageNumberStart;
            PageNumberEnd = pageNumberEnd;
        }
    }
}
