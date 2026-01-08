using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.NutritionFactsLabel
{
    /// <summary>
    ///     Nutrition Facts Label API version 1.0 document data.
    /// </summary>
    public class NutritionFactsLabelV1Document : IPrediction
    {
        /// <summary>
        ///     The amount of added sugars in the product.
        /// </summary>
        [JsonPropertyName("added_sugars")]
        public NutritionFactsLabelV1AddedSugar AddedSugars { get; set; }

        /// <summary>
        ///     The amount of calories in the product.
        /// </summary>
        [JsonPropertyName("calories")]
        public NutritionFactsLabelV1Calorie Calories { get; set; }

        /// <summary>
        ///     The amount of cholesterol in the product.
        /// </summary>
        [JsonPropertyName("cholesterol")]
        public NutritionFactsLabelV1Cholesterol Cholesterol { get; set; }

        /// <summary>
        ///     The amount of dietary fiber in the product.
        /// </summary>
        [JsonPropertyName("dietary_fiber")]
        public NutritionFactsLabelV1DietaryFiber DietaryFiber { get; set; }

        /// <summary>
        ///     The amount of nutrients in the product.
        /// </summary>
        [JsonPropertyName("nutrients")]
        [JsonConverter(typeof(ObjectListJsonConverter<NutritionFactsLabelV1Nutrients, NutritionFactsLabelV1Nutrient>))]
        public NutritionFactsLabelV1Nutrients Nutrients { get; set; }

        /// <summary>
        ///     The amount of protein in the product.
        /// </summary>
        [JsonPropertyName("protein")]
        public NutritionFactsLabelV1Protein Protein { get; set; }

        /// <summary>
        ///     The amount of saturated fat in the product.
        /// </summary>
        [JsonPropertyName("saturated_fat")]
        public NutritionFactsLabelV1SaturatedFat SaturatedFat { get; set; }

        /// <summary>
        ///     The number of servings in each box of the product.
        /// </summary>
        [JsonPropertyName("serving_per_box")]
        public AmountField ServingPerBox { get; set; }

        /// <summary>
        ///     The size of a single serving of the product.
        /// </summary>
        [JsonPropertyName("serving_size")]
        public NutritionFactsLabelV1ServingSize ServingSize { get; set; }

        /// <summary>
        ///     The amount of sodium in the product.
        /// </summary>
        [JsonPropertyName("sodium")]
        public NutritionFactsLabelV1Sodium Sodium { get; set; }

        /// <summary>
        ///     The total amount of carbohydrates in the product.
        /// </summary>
        [JsonPropertyName("total_carbohydrate")]
        public NutritionFactsLabelV1TotalCarbohydrate TotalCarbohydrate { get; set; }

        /// <summary>
        ///     The total amount of fat in the product.
        /// </summary>
        [JsonPropertyName("total_fat")]
        public NutritionFactsLabelV1TotalFat TotalFat { get; set; }

        /// <summary>
        ///     The total amount of sugars in the product.
        /// </summary>
        [JsonPropertyName("total_sugars")]
        public NutritionFactsLabelV1TotalSugar TotalSugars { get; set; }

        /// <summary>
        ///     The amount of trans fat in the product.
        /// </summary>
        [JsonPropertyName("trans_fat")]
        public NutritionFactsLabelV1TransFat TransFat { get; set; }

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($":Serving per Box: {ServingPerBox}\n");
            result.Append($":Serving Size:{ServingSize.ToFieldList()}");
            result.Append($":Calories:{Calories.ToFieldList()}");
            result.Append($":Total Fat:{TotalFat.ToFieldList()}");
            result.Append($":Saturated Fat:{SaturatedFat.ToFieldList()}");
            result.Append($":Trans Fat:{TransFat.ToFieldList()}");
            result.Append($":Cholesterol:{Cholesterol.ToFieldList()}");
            result.Append($":Total Carbohydrate:{TotalCarbohydrate.ToFieldList()}");
            result.Append($":Dietary Fiber:{DietaryFiber.ToFieldList()}");
            result.Append($":Total Sugars:{TotalSugars.ToFieldList()}");
            result.Append($":Added Sugars:{AddedSugars.ToFieldList()}");
            result.Append($":Protein:{Protein.ToFieldList()}");
            result.Append($":sodium:{Sodium.ToFieldList()}");
            result.Append($":nutrients:{Nutrients}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
