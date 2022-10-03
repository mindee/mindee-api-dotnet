using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Invoice
{
    public class DueDate : BaseField
    {
        public string Raw { get; set; }

        public string Value { get; set; }
    }
}
