﻿namespace Mindee.Prediction.Commun
{
    /// <summary>
    /// The local of the page.
    /// </summary>
    public class Locale
    {
        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        public double Confidence { get; set; }

        /// <summary>
        /// The currency which has been detected.
        /// </summary>
        /// <example>EUR</example>
        public string Currency { get; set; }

        /// <summary>
        /// The langage which has been detected.
        /// </summary>
        /// <example>fr</example>
        public string Language { get; set; }

        /// <summary>
        /// The country which has been detected.
        /// </summary>
        /// <example>FR</example>
        public string Country { get; set; }
    }
}
