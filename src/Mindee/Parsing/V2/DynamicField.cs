namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Possible field types.
    /// </summary>
    public enum FieldType
    {
        /// <summary>
        /// Simple field.
        /// </summary>
        SimpleField,
        /// <summary>
        /// Object field.
        /// </summary>
        ObjectField,
        /// <summary>
        /// List field.
        /// </summary>
        ListField,
    }

    /// <summary>
    /// Return the field class dynamically.
    /// </summary>
    public class DynamicField
    {
        /// <summary>
        /// The type of field.
        /// </summary>
        public FieldType Type;

        /// <summary>
        /// Value as simple field.
        /// </summary>
        public SimpleField SimpleField;

        /// <summary>
        /// Value as list field.
        /// </summary>
        public ListField ListField;

        /// <summary>
        /// Value as object field.
        /// </summary>
        public ObjectField ObjectField;

        /// <summary>
        /// Return the field class dynamically.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="simpleField"></param>
        /// <param name="listField"></param>
        /// <param name="objectField"></param>
        public DynamicField(
            FieldType type,
            SimpleField simpleField = null,
            ListField listField = null,
            ObjectField objectField = null
            )
        {
            Type = type;
            SimpleField = simpleField;
            ListField = listField;
            ObjectField = objectField;
        }
    }
}
