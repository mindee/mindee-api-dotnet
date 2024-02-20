using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Resume
{
    /// <summary>
    /// The list of URLs for social network profiles of the person.
    /// </summary>
    public sealed class ResumeV1SocialNetworksUrl : LineItemField
    {
        /// <summary>
        /// The name of of the social media concerned.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The URL of the profile for this particular social network.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "| "
              + String.Format("{0,-20}", printable["Name"])
              + " | "
              + String.Format("{0,-50}", printable["Url"])
              + " |";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Name: "
              + printable["Name"]
              + ", URL: "
              + printable["Url"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Name", SummaryHelper.FormatString(Name, 20)},
                {"Url", SummaryHelper.FormatString(Url, 50)},
            };
        }
    }

    /// <summary>
    /// The list of URLs for social network profiles of the person.
    /// </summary>
    public class ResumeV1SocialNetworksUrls : List<ResumeV1SocialNetworksUrl>
    {
        /// <summary>
        /// Default string representation.
        /// </summary>
        public override string ToString()
        {
            if (this.Count == 0)
            {
                return "\n";
            }
            int[] columnSizes = { 22, 52 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Name                 ");
            outStr.Append("| URL                                                ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
