using System.Text.Json.Nodes;
using Mindee.V1.Parsing.Common;

namespace Mindee.V1.Exceptions
{
    /// <summary>Bad Request (400).</summary>
    public sealed class MindeeHttp400ExceptionV1 : MindeeHttpExceptionV1
    {
        /// <inheritdoc />
        public MindeeHttp400ExceptionV1(string name, string message, ErrorDetails details)
            : base(name, message, details, 400) { }
    }

    /// <summary>Unauthorized — invalid or missing API key (401).</summary>
    public sealed class MindeeHttp401ExceptionV1 : MindeeHttpExceptionV1
    {
        /// <inheritdoc />
        public MindeeHttp401ExceptionV1(string name, string message, ErrorDetails details)
            : base(name, message, details, 401) { }
    }

    /// <summary>Forbidden — API key lacks permission (403).</summary>
    public sealed class MindeeHttp403ExceptionV1 : MindeeHttpExceptionV1
    {
        /// <inheritdoc />
        public MindeeHttp403ExceptionV1(string name, string message, ErrorDetails details)
            : base(name, message, details, 403) { }
    }

    /// <summary>Not Found (404).</summary>
    public sealed class MindeeHttp404ExceptionV1 : MindeeHttpExceptionV1
    {
        /// <inheritdoc />
        public MindeeHttp404ExceptionV1(string name, string message, ErrorDetails details)
            : base(name, message, details, 404) { }
    }

    /// <summary>Document too large (413).</summary>
    public sealed class MindeeHttp413ExceptionV1 : MindeeHttpExceptionV1
    {
        /// <inheritdoc />
        public MindeeHttp413ExceptionV1(string name, string message, ErrorDetails details)
            : base(name, message, details, 413) { }
    }

    /// <summary>Too Many Requests — rate limit exceeded (429).</summary>
    public sealed class MindeeHttp429ExceptionV1 : MindeeHttpExceptionV1
    {
        /// <inheritdoc />
        public MindeeHttp429ExceptionV1(string name, string message, ErrorDetails details)
            : base(name, message, details, 429) { }
    }

    /// <summary>Internal Server Error (500).</summary>
    public sealed class MindeeHttp500ExceptionV1 : MindeeHttpExceptionV1
    {
        /// <inheritdoc />
        public MindeeHttp500ExceptionV1(string name, string message, ErrorDetails details)
            : base(name, message, details, 500) { }
    }

    /// <summary>Gateway Timeout / Request Timeout (504).</summary>
    public sealed class MindeeHttp504ExceptionV1 : MindeeHttpExceptionV1
    {
        /// <inheritdoc />
        public MindeeHttp504ExceptionV1(string name, string message, ErrorDetails details)
            : base(name, message, details, 504) { }
    }
}
