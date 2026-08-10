using System.Text.Json.Serialization;
using Mindee.V2.Parsing.Inference.Field;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    /// Return the field class dynamically.
    /// </summary>
    [JsonConverter(typeof(DynamicAnnotationFieldJsonConverter))]
    public class AnnotatedDynamicField
    {
        /// <summary>
        /// Value as a simple field.
        /// </summary>
        public AnnotatedSimpleField SimpleField { get; set; }

        /// <summary>
        /// Value as an object field.
        /// </summary>
        public AnnotatedObjectField ObjectField { get; set; }

        /// <summary>
        /// Value as a list field.
        /// </summary>
        public AnnotatedListField ListField { get; set; }

        /// <summary>
        /// The type of field.
        /// </summary>
        public FieldType Type { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="simpleField"></param>
        /// <param name="objectField"></param>
        /// <param name="listField"></param>
        public AnnotatedDynamicField(
            FieldType type
            , AnnotatedSimpleField simpleField = null
            , AnnotatedObjectField objectField = null
            , AnnotatedListField listField = null)
        {
            SimpleField = simpleField;
            ObjectField = objectField;
            ListField = listField;
            Type = type;
        }
    }
}
