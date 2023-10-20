using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Mindee.Parsing.Standard;

namespace Mindee.Parsing
{
    internal static class SummaryHelper
    {
        public static string Clean(string summary)
        {
            Regex cleanSpace = new Regex(" \n", RegexOptions.Multiline);
            return cleanSpace.Replace(summary, "\n");
        }

        public static string FormatAmount(double? amount)
        {
            return amount == null ? "" : amount.Value.ToString("0.00###");
        }

        public static string FormatAmount(decimal? amount)
        {
            return amount == null ? "" : amount.Value.ToString("0.00###");
        }

        public static string FormatString(string str)
        {
            return str ?? "";
        }

        public static string FormatString(string str, int maxLength)
        {
            String strSummary = FormatString(str);
            if (strSummary.Length > maxLength)
            {
                strSummary = strSummary.Substring(0, maxLength - 3) + "...";
            }
            return strSummary;
        }

        /// <summary>
        /// Format an rST table line separator.
        /// </summary>
        public static string LineSeparator(int[] columnSizes, char str)
        {
            StringBuilder outStr = new StringBuilder("+");

            foreach (var size in columnSizes)
            {
                outStr.Append(new String(str, size) + "+");
            }
            outStr.Append('\n');
            return outStr.ToString();
        }

        public static String ArrayToString<T>(List<T> lineItems, int[] columnSizes)
        where T : LineItemField, new()
        {
            StringBuilder outStr = new StringBuilder();
            foreach (var lineItem in lineItems)
            {
                outStr.Append("  " + lineItem.ToTableLine() + '\n');
                outStr.Append("  " + LineSeparator(columnSizes, '-'));
            }
            return outStr.ToString();
        }
    }
}
