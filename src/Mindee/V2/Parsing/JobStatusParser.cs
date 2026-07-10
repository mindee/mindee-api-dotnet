using System;

namespace Mindee.V2.Parsing
{
    internal static class JobStatusParser
    {
        public static JobStatus Parse(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return JobStatus.Unknown;
            }

            if (Enum.TryParse(status, true, out JobStatus parsedStatus))
            {
                return parsedStatus;
            }

            return JobStatus.Unknown;
        }
    }
}
