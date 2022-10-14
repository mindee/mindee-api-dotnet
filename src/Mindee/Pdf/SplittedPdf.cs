namespace Mindee.Pdf
{
    public sealed class SplittedPdf
    {
        public byte[] File { get; }
        public ushort TotalPageNumber { get; }

        public SplittedPdf(byte[] file, ushort totalPageNumber)
        {
            File = file;
            TotalPageNumber = totalPageNumber;
        }
    }
}
