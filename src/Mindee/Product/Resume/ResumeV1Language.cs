using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Resume
{
    /// <summary>
    ///     The list of languages that the candidate is proficient in.
    /// </summary>
    public sealed class ResumeV1Language : LineItemField
    {
        /// <summary>
        ///     The language's ISO 639 code.
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        ///     The candidate's level for the language.
        /// </summary>
        [JsonPropertyName("level")]
        public string Level { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "Language", SummaryHelper.FormatString(Language) },
                { "Level", SummaryHelper.FormatString(Level, 20) }
            };
        }

        /// <summary>
        ///     Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            var printable = TablePrintableValues();
            return "| "
                   + string.Format("{0,-8}", printable["Language"])
                   + " | "
                   + string.Format("{0,-20}", printable["Level"])
                   + " |";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Language: "
                   + printable["Language"]
                   + ", Level: "
                   + printable["Level"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "Language", SummaryHelper.FormatString(Language) }, { "Level", SummaryHelper.FormatString(Level) }
            };
        }
    }

    /// <summary>
    ///     The list of languages that the candidate is proficient in.
    /// </summary>
    public class ResumeV1Languages : List<ResumeV1Language>
    {
        /// <summary>
        ///     Default string representation.
        /// </summary>
        public override string ToString()
        {
            if (Count == 0)
            {
                return "\n";
            }

            int[] columnSizes = { 10, 22 };
            var outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Language ");
            outStr.Append("| Level                ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
