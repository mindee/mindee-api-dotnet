using System;
using System.Collections.Generic;
using Mindee.Parsing.Common;

namespace Mindee.Exceptions
{
    /// <summary>
    /// Representation of a Mindee API V2 exception.
    /// </summary>
    public class MindeeHttpExceptionV2 : Exception
    {
        /// <summary>
        /// Detail relevant to the error.
        /// </summary>
        public string Detail { get; set; }

        // /// <summary>
        // /// Http error code.
        // /// </summary>
        // public int StatusCode { get; set; }
        //
        // /// <summary>
        // /// Error title.
        // /// </summary>
        // public string Title { get; set; }

        /// <summary>
        /// Specific error code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// List of error sub-objects for more details.
        /// </summary>
        public List<MindeeHttpExceptionError> Errors { get; set; }

        /// <summary>
        /// Initializes a new instance of the MindeeHttpException class using the provided Error object.
        /// </summary>
        /// <param name="error">Contents of the error object.</param>
        public MindeeHttpExceptionV2(ErrorV2 error) : base(error.Detail)
        {
            Detail = error.Detail;
            Code = error.Code;
            // StatusCode = error.Status;
            // Title = error.Title;
            if (error.Errors != null && error.Errors.Count != 0)
            {
                // Errors = [];
                // foreach (var subError in error.Errors)
                // {
                //     Errors.Add(new MindeeHttpExceptionError { Detail = subError.Detail, Pointer = subError.Pointer });
                // }
            }
            // else
            // {
            //     Errors = [];
            // }
        }
    }
}
