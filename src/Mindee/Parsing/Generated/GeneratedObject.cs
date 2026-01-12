using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Mindee.Geometry;
using Mindee.Parsing.Standard;

namespace Mindee.Parsing.Generated
{
    /// <summary>
    ///     An object within a feature.
    /// </summary>
    public class GeneratedObject : Dictionary<string, JsonElement>
    {
        /// <summary>
        ///     Represent the object as a standard <see cref="StringField" /> object.
        /// </summary>
        /// <returns>A <see cref="StringField" /> object.</returns>
        public StringField AsStringField()
        {
            return new StringField(
                this["value"].GetString(),
                TryGetString("raw_value"),
                Confidence(),
                Polygon(),
                PageId());
        }

        /// <summary>
        ///     Represent the object as a standard <see cref="DecimalField" /> object.
        /// </summary>
        /// <returns>A <see cref="DecimalField" /> object.</returns>
        public DecimalField AsDecimalField()
        {
            return new DecimalField(
                this["value"].GetDecimal(),
                Confidence(),
                Polygon(),
                PageId());
        }

        /// <summary>
        ///     Represent the object as a standard <see cref="AmountField" /> object.
        /// </summary>
        /// <returns>An <see cref="AmountField" /> object.</returns>
        public AmountField AsAmountField()
        {
            return new AmountField(
                this["value"].GetDouble(),
                Confidence(),
                Polygon(),
                PageId());
        }

        /// <summary>
        ///     Represent the object as a standard <see cref="BooleanField" /> object.
        /// </summary>
        /// <returns>A <see cref="BooleanField" /> object.</returns>
        public BooleanField AsBooleanField()
        {
            return new BooleanField(
                this["value"].GetBoolean(),
                Confidence(),
                Polygon(),
                PageId());
        }

        /// <summary>
        ///     Represent the object as a standard <see cref="DateField" /> object.
        /// </summary>
        /// <returns>A <see cref="DateField" /> object.</returns>
        public DateField AsDateField()
        {
            return new DateField(
                this["value"].GetString(),
                Confidence(),
                Polygon(),
                PageId(),
                IsComputed());
        }

        /// <summary>
        ///     Represent the object as a standard <see cref="ClassificationField" /> object.
        /// </summary>
        /// <returns>A <see cref="ClassificationField" /> object.</returns>
        public ClassificationField AsClassificationField()
        {
            return new ClassificationField(this["value"].GetString());
        }

        /// <summary>
        ///     Attempts to represent the specified key as a string.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The value as a string or null if the key doesn't exist.</returns>
        public string TryGetString(string key)
        {
            if (ContainsKey(key))
            {
                return this[key].GetString();
            }

            return null;
        }

        /// <summary>
        ///     Attempts to represent the specified key as an int.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The value as an int or null if the key doesn't exist.</returns>
        public int? TryGetInt(string key)
        {
            if (ContainsKey(key))
            {
                return this[key].GetInt16();
            }

            return null;
        }

        /// <summary>
        ///     Attempts to represent the specified key as a double.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The value as a double or null if the key doesn't exist.</returns>
        public double? TryGetDouble(string key)
        {
            if (ContainsKey(key))
            {
                return this[key].GetDouble();
            }

            return null;
        }

        /// <summary>
        ///     Attempts to represent the specified key as a boolean.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The value as a boolean or null if the key doesn't exist.</returns>
        public bool? TryGetBool(string key)
        {
            if (ContainsKey(key))
            {
                return this[key].GetBoolean();
            }

            return null;
        }

        /// <summary>
        ///     Get the page ID, if present.
        /// </summary>
        /// <returns>The page ID as an int, or null.</returns>
        public int? PageId()
        {
            return TryGetInt("page_id");
        }

        /// <summary>
        ///     Get the confidence score, if present.
        /// </summary>
        /// <returns>The confidence score as a double, or null.</returns>
        public double? Confidence()
        {
            return TryGetDouble("confidence");
        }

        /// <summary>
        ///     Get the 'is_computed' key, if present.
        /// </summary>
        /// <returns>True if the field was computed, false if it was present, null otherwise.</returns>
        public bool? IsComputed()
        {
            return TryGetBool("is_computed");
        }

        /// <summary>
        ///     Get the polygon, if present.
        /// </summary>
        public Polygon Polygon()
        {
            return TryGetPolygon("polygon");
        }

        /// <summary>
        ///     Get the specified key as a <see cref="Mindee.Geometry.Polygon" /> object.
        /// </summary>
        public Polygon TryGetPolygon(string key)
        {
            if (ContainsKey(key))
            {
                return ConvertElementToPolygon(this[key]);
            }

            return null;
        }

        /// <summary>
        ///     Convert a JsonElement to a Polygon.
        /// </summary>
        /// <param name="element">The JSON element to convert.</param>
        /// <returns>A <see cref="Mindee.Geometry.Polygon" /> object.</returns>
        public static Polygon ConvertElementToPolygon(JsonElement element)
        {
            var points = new List<List<double>>();
            foreach (var doubles in element.EnumerateArray())
            {
                var point = new Point(doubles[0].GetDouble(), doubles[1].GetDouble());
                points.Add(point);
            }

            return new Polygon(points);
        }

        /// <summary>
        ///     A prettier representation of the feature values.
        /// </summary>
        public override string ToString()
        {
            return ToString(0);
        }

        /// <summary>
        ///     A prettier representation of the feature values, with offset.
        /// </summary>
        public string ToString(int offset)
        {
            var padding = new string(' ', offset);
            var result = new StringBuilder();

            foreach (var field in this)
            {
                result.Append($"{padding}:{field.Key}: {field.Value}\n");
            }

            return result.ToString();
        }
    }
}
