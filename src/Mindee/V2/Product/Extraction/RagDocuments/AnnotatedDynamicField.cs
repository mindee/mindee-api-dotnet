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
        /// Constructor for a simple field.
        /// </summary>
        /// <param name="field"><see cref="AnnotatedSimpleField"/></param>
        public AnnotatedDynamicField(AnnotatedSimpleField field)
        {
            SimpleField = field;
            ObjectField = null;
            ListField = null;
            Type = FieldType.SimpleField;
        }

        /// <summary>
        /// Constructor for a list field.
        /// </summary>
        /// <param name="field"><see cref="AnnotatedSimpleField"/></param>
        public AnnotatedDynamicField(AnnotatedListField field)
        {
            SimpleField = null;
            ObjectField = null;
            ListField = field;
            Type = FieldType.ListField;
        }

        /// <summary>
        /// Constructor for an object field.
        /// </summary>
        /// <param name="field"><see cref="AnnotatedObjectField"/></param>
        public AnnotatedDynamicField(AnnotatedObjectField field)
        {
            SimpleField = null;
            ObjectField = field;
            ListField = null;
            Type = FieldType.ObjectField;
        }
    }
}
