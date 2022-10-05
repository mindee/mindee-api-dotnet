﻿using System.Threading.Tasks;
using Mindee.Domain.Parsing.Common;
using Mindee.Domain.Parsing.Invoice;

namespace Mindee.Domain.Parsing
{
    /// <summary>
    /// Parse a file. 
    /// </summary>
    public interface IInvoiceParsing
    {
        /// <summary>
        /// Execute the parsing.
        /// </summary>
        /// <param name="parseParameter"><see cref="ParseParameter"/></param>
        /// <returns><see cref="InvoiceInference"/></returns>
        Task<Document<InvoicePrediction>> ExecuteAsync(ParseParameter parseParameter);
    }
}