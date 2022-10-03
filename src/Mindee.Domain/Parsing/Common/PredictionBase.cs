namespace Mindee.Domain.Parsing.Common
{
    public abstract class PredictionBase
    {
        /// <summary>
        /// <see cref="Locale"/>
        /// </summary>
        public Locale Locale { get; set; }

        /// <summary>
        /// <see cref="Orientation"/>
        /// </summary>
        public Orientation Orientation { get; set; }
    }
}
