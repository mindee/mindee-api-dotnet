using Mindee.Http;
using Mindee.Parsing.Common;
using Mindee.Product.Generated;

namespace Mindee.UnitTests.V1.Product.Generated
{
    [Trait("Category", "Generated API")]
    public class GeneratedV1Test
    {
        [Fact]
        public async Task SyncPredict_MustSuccessfullyHandleMultiplePages()
        {
            var response = await GetSyncPrediction("complete");
            Assert.Equal(2, response.Document.Inference.Pages.Count);
        }

        [Fact]
        public async Task AsyncPredict_WhenEmpty_MustHaveValidProperties()
        {
            var response = await GetAsyncPrediction("empty");
            var features = response.Document.Inference.Prediction.Fields;
            Assert.Equal(15, features.Count);
            Assert.False(features["birth_date"].IsList);
            Assert.True(features["surnames"].IsList);

            foreach (var field in features)
            {
                if (field.Value.IsList)
                {
                    Assert.Empty(field.Value);
                }
                else
                {
                    Assert.Null(field.Value.First()["value"].GetString());
                }
            }
        }

        [Fact]
        public async Task AsyncPredict_WhenComplete_MustHaveValidProperties()
        {
            var response = await GetAsyncPrediction("complete");
            var features = response.Document.Inference.Prediction.Fields;
            Assert.Equal(15, features.Count);

            // Direct access to the dictionary

            Assert.False(features["address"].IsList);
            Assert.Equal("AVDA DE MADRID S-N MADRID MADRID", features["address"].First()["value"].GetString());

            Assert.False(features["birth_date"].IsList);
            Assert.Equal("1980-01-01", features["birth_date"].First()["value"].GetString());

            Assert.False(features["birth_place"].IsList);
            Assert.Equal("MADRID", features["birth_place"].First()["value"].GetString());

            Assert.False(features["country_of_issue"].IsList);
            Assert.Equal("ESP", features["country_of_issue"].First()["value"].GetString());

            Assert.False(features["document_number"].IsList);
            Assert.Equal("99999999R", features["document_number"].First()["value"].GetString());

            Assert.True(features["given_names"].IsList);
            Assert.Equal("CARMEN", features["given_names"].First()["value"].GetString());

            Assert.True(features["surnames"].IsList);
            Assert.Equal("ESPAÑOLA", features["surnames"][0]["value"].GetString());
            Assert.Equal("ESPAÑOLA", features["surnames"][1]["value"].GetString());

            // Access as a StringField without raw_value
            var addressField = features["address"].AsStringField();
            Assert.Equal("AVDA DE MADRID S-N MADRID MADRID", addressField.Value);
            Assert.Null(addressField.RawValue);
            Assert.Null(addressField.Confidence);
            Assert.Null(addressField.Polygon);

            // Access as a DateField
            var expiryDateField = features["expiry_date"].AsDateField();
            Assert.Equal("2025-01-01", expiryDateField.Value);
            Assert.Equal("2025-01-01", expiryDateField.DateObject?.ToString("yyyy-MM-dd"));
        }

        [Fact]
        public async Task SyncPredict_WhenEmpty_MustHaveValidProperties()
        {
            var response = await GetSyncPrediction("empty");
            var features = response.Document.Inference.Prediction.Fields;
            Assert.Equal(17, features.Count);
            Assert.False(features["date"].IsList);
            Assert.True(features["line_items"].IsList);
        }

        [Fact]
        public async Task SyncPredict_WhenComplete_MustHaveValidProperties()
        {
            var response = await GetSyncPrediction("complete");
            var features = response.Document.Inference.Prediction.Fields;
            Assert.Equal(17, features.Count);

            // Direct access to the dictionary
            var customerName = features["customer_name"];
            Assert.False(customerName.IsList);
            Assert.Equal("JIRO DOI", customerName.First()["value"].ToString());
            Assert.Equal("JIRO DOI", customerName.First().TryGetString("value"));
            Assert.Equal("Jiro Doi", customerName.First()["raw_value"].ToString());
            Assert.Equal("Jiro Doi", customerName.First()["raw_value"].GetString());
            Assert.Equal(0.87, customerName.First()["confidence"].GetDouble());
            Assert.Equal(1, customerName.First()["page_id"].GetInt16());
            Assert.Equal(
                "Polygon with 4 points.",
                customerName.First().TryGetPolygon("polygon").ToString());

            // Access as a StringField with raw_value
            var customerNameField = customerName.AsStringField();
            Assert.Equal("JIRO DOI", customerNameField.Value);
            Assert.Equal("Jiro Doi", customerNameField.RawValue);
            Assert.Equal(0.87, customerNameField.Confidence);
            Assert.Equal(1, customerNameField.PageId);

            // Access as a StringField without raw_value
            var supplierAddressField = features["supplier_address"].AsStringField();
            Assert.Equal("156 University Ave, Toronto ON, Canada M5H 2H7", supplierAddressField.Value);
            Assert.Null(supplierAddressField.RawValue);
            Assert.Equal(0.53, supplierAddressField.Confidence);
            Assert.Equal(1, supplierAddressField.PageId);
            Assert.Equal("Polygon with 4 points.", supplierAddressField.Polygon.ToString());

            // Access as an AmountField
            var totalAmountField = features["total_amount"].AsAmountField();
            Assert.Equal(587.95, totalAmountField.Value);

            // Access as a DateField
            var dueDateField = features["due_date"].AsDateField();
            Assert.Equal("2020-02-17", dueDateField.Value);
            Assert.Equal("2020-02-17", dueDateField.DateObject?.ToString("yyyy-MM-dd"));

            // Access as a ClassificationField
            var documentTypeField = features["document_type"].AsClassificationField();
            Assert.Equal("INVOICE", documentTypeField.Value);

            // Access line items
            var lineItems = features["line_items"];
            Assert.True(lineItems.IsList);
            foreach (var lineItem in lineItems)
            {
                Assert.NotNull(lineItem["description"].GetString());
            }

            var firstLineItem = lineItems.First();
            Assert.Equal(0.84, firstLineItem["confidence"].GetDouble());
            Assert.Equal("S)BOIE 5X500 FEUILLES A4", firstLineItem["description"].GetString());
            Assert.Equal(0, firstLineItem["page_id"].GetInt16());
            Assert.Null(firstLineItem["product_code"].GetString());
        }

        // Edge cases: booleans & int amounts.

        [Fact]
        public async Task ReceiptsItemsClassifierPredict_WhenComplete_MustHaveValidIntField()
        {
            var response = await GetReceiptsItemsClassifierPrediction();
            var features = response.Document.Inference.Prediction.Fields;
            Assert.Equal(1.0, features["line_items"].First()["quantity"].GetDouble());
        }

        [Fact]
        public async Task UsMailPredict_WhenComplete_MustHaveValidBooleanField()
        {
            var response = await GetUsMailPrediction();
            var features = response.Document.Inference.Prediction.Fields;
            Assert.False(features["is_return_to_sender"].AsBooleanField().Value);
        }

        private static async Task<AsyncPredictResponse<GeneratedV1>> GetReceiptsItemsClassifierPrediction()
        {
            var fileName = Constants.V1RootDir + "products/receipts_items_classifier/response_v1/complete.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictAsyncPostAsync<GeneratedV1>(
                UnitTestBase.GetFakePredictParameter()
                , new CustomEndpoint("receipts_items_classifier", "mindee"));
        }

        private static async Task<AsyncPredictResponse<GeneratedV1>> GetUsMailPrediction()
        {
            var fileName = Constants.V1RootDir + "products/us_mail/response_v3/complete.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictAsyncPostAsync<GeneratedV1>(
                UnitTestBase.GetFakePredictParameter()
                , new CustomEndpoint("us_mail", "mindee", "3"));
        }

        private static async Task<AsyncPredictResponse<GeneratedV1>> GetAsyncPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/generated/response_v1/{name}_international_id_v1.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictAsyncPostAsync<GeneratedV1>(
                UnitTestBase.GetFakePredictParameter()
                , new CustomEndpoint("international_id", "mindee", "2"));
        }

        private static Task<PredictResponse<GeneratedV1>> GetSyncPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/generated/response_v1/{name}_invoice_v4.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return mindeeAPi.PredictPostAsync<GeneratedV1>(
                UnitTestBase.GetFakePredictParameter()
                , new CustomEndpoint("invoice", "mindee", "4"));
        }
    }
}
