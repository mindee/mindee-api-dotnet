﻿using System.Collections.Generic;

namespace Mindee.Domain.Parsing.Common
{
    public abstract class BaseField
    {
        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        public double Confidence { get; set; }

        /// <summary>
        /// Define the coordinates of the zone in the page where the values has been found.
        /// </summary>
        public List<List<double>> Polygon { get; set; }

        public int? PageId { get; set; }

    }
}