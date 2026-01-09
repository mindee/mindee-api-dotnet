using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindee.Exceptions;
using Mindee.Parsing.Standard;

namespace Mindee.Parsing.Generated
{
    /// <summary>
    ///     A generic feature which can represent any OTS Mindee return prediction.
    /// </summary>
    public class GeneratedFeature : List<GeneratedObject>
    {
        /// <summary>
        ///     Default constructor.
        /// </summary>
        /// <param name="isList">
        ///     <see cref="IsList" />
        /// </param>
        public GeneratedFeature(bool isList)
        {
            IsList = isList;
        }

        /// <summary>
        ///     Whether the original feature is a list.
        /// </summary>
        public bool IsList { get; set; }

        /// <summary>
        ///     Represent the feature as a standard <see cref="StringField" /> object.
        /// </summary>
        /// <returns>A <see cref="StringField" /> object.</returns>
        /// <exception cref="MindeeException">If the feature is a list.</exception>
        public StringField AsStringField()
        {
            if (IsList)
            {
                throw new MindeeException("Cannot convert a list feature into a StringField.");
            }

            return this.First().AsStringField();
        }

        /// <summary>
        ///     Represent the feature as a standard <see cref="AmountField" /> object.
        /// </summary>
        /// <returns>An <see cref="AmountField" /> object.</returns>
        /// <exception cref="MindeeException">If the feature is a list.</exception>
        public AmountField AsAmountField()
        {
            if (IsList)
            {
                throw new MindeeException("Cannot convert a list feature into an AmountField.");
            }

            return this.First().AsAmountField();
        }

        /// <summary>
        ///     Represent the feature as a standard <see cref="DecimalField" /> object.
        /// </summary>
        /// <returns>A <see cref="DecimalField" /> object.</returns>
        /// <exception cref="MindeeException">If the feature is a list.</exception>
        public DecimalField AsDecimalField()
        {
            if (IsList)
            {
                throw new MindeeException("Cannot convert a list feature into a DecimalField.");
            }

            return this.First().AsDecimalField();
        }

        /// <summary>
        ///     Represent the feature as a standard <see cref="DateField" /> object.
        /// </summary>
        /// <returns>A <see cref="DateField" /> object.</returns>
        /// <exception cref="MindeeException">If the feature is a list.</exception>
        public DateField AsDateField()
        {
            if (IsList)
            {
                throw new MindeeException("Cannot convert a list feature into a DateField.");
            }

            return this.First().AsDateField();
        }

        /// <summary>
        ///     Represent the feature as a standard <see cref="ClassificationField" /> object.
        /// </summary>
        /// <returns>A <see cref="ClassificationField" /> object.</returns>
        /// <exception cref="MindeeException">If the feature is a list.</exception>
        public ClassificationField AsClassificationField()
        {
            if (IsList)
            {
                throw new MindeeException("Cannot convert a list feature into a ClassificationField.");
            }

            return this.First().AsClassificationField();
        }

        /// <summary>
        ///     Represent the feature as a standard <see cref="BooleanField" /> object.
        /// </summary>
        /// <returns>A <see cref="BooleanField" /> object.</returns>
        /// <exception cref="MindeeException"></exception>
        public BooleanField AsBooleanField()
        {
            if (IsList)
            {
                throw new MindeeException("Cannot convert a list feature into a BooleanField.");
            }

            return this.First().AsBooleanField();
        }

        /// <summary>
        ///     A prettier representation of the feature values.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();

            if (IsList)
            {
                for (var i = 0; i < Count; i++)
                {
                    if (i == 0)
                    {
                        result.Append($"\n  * {this[i]}\n");
                    }
                    else
                    {
                        result.Append($"\n{this[i].ToString(4)}\n");
                    }
                }
            }
            else
            {
                result.Append($"\n{this.First().ToString(2)}");
            }

            return result.ToString();
        }
    }
}
