using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    /// A ListField with additional configuration for annotation.
    /// </summary>
    public class AnnotatedListField : AnnotatedBaseField
    {
        private List<AnnotatedObjectField> _objectItems;

        private List<AnnotatedSimpleField> _simpleItems;

        /// <summary>
        ///     List of dynamic fields, prefer SimpleItems or ObjectItems.
        /// </summary>
        [JsonPropertyName("items")]
        public List<AnnotatedDynamicField> Items { get; set; } = [];

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="guidelines"></param>
        public AnnotatedListField(bool selected, string guidelines) : base(selected, guidelines)
        {
        }

        /// <summary>
        ///     List of simple fields.
        /// </summary>
        [JsonIgnore]
        public List<AnnotatedSimpleField> SimpleItems
        {
            get
            {
                if (_simpleItems != null)
                {
                    return _simpleItems;
                }

                _simpleItems = [];
                foreach (var item in Items.Where(item => item.SimpleField != null))
                {
                    _simpleItems.Add(item.SimpleField);
                }

                return _simpleItems;
            }
        }

        /// <summary>
        ///     List of object fields.
        /// </summary>
        [JsonIgnore]
        public List<AnnotatedObjectField> ObjectItems
        {
            get
            {
                if (_objectItems != null)
                {
                    return _objectItems;
                }

                _objectItems = [];
                foreach (var item in Items.Where(item => item.ObjectField != null))
                {
                    _objectItems.Add(item.ObjectField);
                }

                return _objectItems;
            }
        }

    }
}
