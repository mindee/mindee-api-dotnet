namespace Mindee.Infrastructure.Api
{
    internal sealed class PredictParameter
    {
        public byte[] File { get; }
        public string Filename { get; }
        public bool WithFullText { get; }

        public PredictParameter(
            byte[] file, 
            string filename)
            : this (file, filename, false)
        {
        }

        public PredictParameter(
            byte[] file,
            string filename,
            bool withFullText) 
        {
            File = file;
            Filename = filename;
            WithFullText = withFullText;
        }
    }
}
