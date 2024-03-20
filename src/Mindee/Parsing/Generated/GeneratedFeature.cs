using System.Collections.Generic;
using System.Linq;
using Mindee.Exceptions;
using Mindee.Parsing.Standard;

namespace Mindee.Parsing.Generated
{
    /// <summary>
    /// A generic feature which can represent any OTS Mindee return prediction.
    /// </summary>
    public class GeneratedFeature : List<GeneratedObject>
    {
        /// <summary>
        /// Whether the original feature is a list.
        /// </summary>
        public bool IsList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="isList"><see cref="IsList"/></param>
        public GeneratedFeature(bool isList)
        {
            IsList = isList;
        }

        /// <summary>
        /// Represent the feature as a standard <see cref="StringField"/> object.
        /// </summary>
        /// <returns>A <see cref="StringField"/> object.</returns>
        /// <exception cref="MindeeException">If the feature is a list.</exception>
        public StringField AsStringField()
        {
            if (this.IsList)
                throw new MindeeException("Cannot convert a list feature into a StringField.");
            return this.First().AsStringField();
        }

        /// <summary>
        /// Represent the feature as a standard <see cref="AmountField"/> object.
        /// </summary>
        /// <returns>An <see cref="AmountField"/> object.</returns>
        /// <exception cref="MindeeException">If the feature is a list.</exception>
        public AmountField AsAmountField()
        {
            if (this.IsList)
                throw new MindeeException("Cannot convert a list feature into an AmountField.");
            return this.First().AsAmountField();
        }

        /// <summary>
        /// Represent the feature as a standard <see cref="DecimalField"/> object.
        /// </summary>
        /// <returns>A <see cref="DecimalField"/> object.</returns>
        /// <exception cref="MindeeException">If the feature is a list.</exception>
        public DecimalField AsDecimalField()
        {
            if (this.IsList)
                throw new MindeeException("Cannot convert a list feature into a DecimalField.");
            return this.First().AsDecimalField();
        }

        /// <summary>
        /// Represent the feature as a standard <see cref="DateField"/> object.
        /// </summary>
        /// <returns>A <see cref="DateField"/> object.</returns>
        /// <exception cref="MindeeException">If the feature is a list.</exception>
        public DateField AsDateField()
        {
            if (this.IsList)
                throw new MindeeException("Cannot convert a list feature into a DateField.");
            return this.First().AsDateField();
        }

        /// <summary>
        /// Represent the feature as a standard <see cref="ClassificationField"/> object.
        /// </summary>
        /// <returns>A <see cref="ClassificationField"/> object.</returns>
        /// <exception cref="MindeeException">If the feature is a list.</exception>
        public ClassificationField AsClassificationField()
        {
            if (this.IsList)
                throw new MindeeException("Cannot convert a list feature into a ClassificationField.");
            return this.First().AsClassificationField();
        }
    }
}
