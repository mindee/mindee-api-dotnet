using Mindee.V2.Parsing;
using Mindee.V2.Product.Extraction.RagDocuments;
using Mindee.V2.Product.Extraction.RagDocuments.Params;

namespace Mindee.UnitTests.V2.Product.Extraction
{
    [Trait("Category", "V2")]
    [Trait("Category", "ExtractionRagDocuments")]
    public class RagDocumentsTest
    {
        [Fact]
        public void PostParameters_MustInit()
        {
            var parameters = new RagDocumentUploadParameters(modelId: "invalid-model-id");
            var reqParams = parameters.GetRequestParameters();
            Assert.Equal("invalid-model-id", reqParams["model_id"]);
        }

        [Fact]
        public void PatchParameters_MustInit()
        {
            var annotation = new RagAnnotation();
            var parameters = new RagDocumentAnnotationParameters(
                documentId: "invalid-document-id"
                , status: "Active"
                , annotation: annotation);
            var reqParams = parameters.GetRequestParameters();
            Assert.Equal("invalid-document-id", parameters.DocumentId);
            Assert.Equal("Active", reqParams["status"]);
            Assert.Equal(annotation, reqParams["annotation"]);
        }

        [Fact]
        public void RagDocumentsPost_MustHaveValidProperties()
        {
            var response = GetResponse("extraction/rag_documents/post_response.json");
            Assert.NotNull(response);
            Assert.Equal("cc831599-c545-48b7-aa27-6d7ccd5b8d32", response.Id);
            Assert.Equal("Processing", response.Status);
            Assert.Null(response.Annotation);
        }

        [Fact]
        public void RagDocumentsGetDraft_MustHaveValidProperties()
        {
            var response = GetResponse("extraction/rag_documents/get_response_draft.json");
            Assert.NotNull(response);
            Assert.Equal("cc831599-c545-48b7-aa27-6d7ccd5b8d32", response.Id);
            Assert.Equal("Draft", response.Status);
            Assert.NotNull(response.Annotation);
            var fields = response.Annotation.Fields;
            Assert.NotNull(fields);

            // null simple field
            var tipField = fields["tip"].SimpleField;
            Assert.NotNull(tipField);
            Assert.False(tipField.Selected);
            Assert.Null(tipField.Guidelines);
            Assert.Null(tipField.Value);

            // filled simple field
            var dateField = fields["date"].SimpleField;
            Assert.NotNull(dateField);
            Assert.False(dateField.Selected);
            Assert.Null(dateField.Guidelines);
            Assert.Equal("2019-11-02", dateField.Value);

            // filled object field
            var localeField = fields["locale"].ObjectField;
            Assert.NotNull(localeField);
            Assert.False(localeField.Selected);
            Assert.Null(localeField.Guidelines);
            Assert.NotNull(localeField.Fields);
            Assert.Equal(3, localeField.Fields.Count);
            Assert.Equal("US", localeField.Fields["country"].SimpleField.Value);
            Assert.Equal("USD", localeField.Fields["currency"].SimpleField.Value);
            Assert.Null(localeField.Fields["language"].SimpleField.Value);

            // list of simple fields
            var referenceNumbersField = fields["reference_numbers"].ListField;
            Assert.NotNull(referenceNumbersField);
            Assert.False(referenceNumbersField.Selected);
            Assert.Null(referenceNumbersField.Guidelines);
            Assert.NotNull(referenceNumbersField.SimpleItems);
            Assert.Single(referenceNumbersField.SimpleItems);
            Assert.Equal("2412/2019", referenceNumbersField.SimpleItems[0].Value);

            // list of object fields
            var lineItemsField = fields["line_items"].ListField;
            Assert.NotNull(lineItemsField);
            Assert.False(lineItemsField.Selected);
            Assert.Null(lineItemsField.Guidelines);
            Assert.NotNull(lineItemsField.ObjectItems);
            Assert.Equal(3, lineItemsField.ObjectItems.Count);

            var lineITem0 = lineItemsField.ObjectItems[0];
            Assert.NotNull(lineITem0.Fields);
            Assert.Equal(8, lineITem0.Fields.Count);
            Assert.Equal("Front and rear brake cables", lineITem0.Fields["description"].SimpleField.Value);
            Assert.Equal(1, lineITem0.Fields["quantity"].SimpleField.Value);
            Assert.Equal(100, lineITem0.Fields["unit_price"].SimpleField.Value);
            Assert.Equal(100, lineITem0.Fields["total_price"].SimpleField.Value);
            Assert.Null(lineITem0.Fields["tax_rate"].SimpleField.Value);
            Assert.Null(lineITem0.Fields["tax_amount"].SimpleField.Value);
            Assert.Null(lineITem0.Fields["product_code"].SimpleField.Value);
            Assert.Null(lineITem0.Fields["unit_measure"].SimpleField.Value);

            var lineITem1 = lineItemsField.ObjectItems[1];
            Assert.NotNull(lineITem1.Fields);
            Assert.Equal(8, lineITem1.Fields.Count);
            Assert.Equal("New set of pedal arms", lineITem1.Fields["description"].SimpleField.Value);
            Assert.Equal(2, lineITem1.Fields["quantity"].SimpleField.Value);
            Assert.Equal(25, lineITem1.Fields["unit_price"].SimpleField.Value);
            Assert.Equal(50, lineITem1.Fields["total_price"].SimpleField.Value);
            Assert.Null(lineITem1.Fields["tax_rate"].SimpleField.Value);
            Assert.Null(lineITem1.Fields["tax_amount"].SimpleField.Value);
            Assert.Null(lineITem1.Fields["product_code"].SimpleField.Value);
            Assert.Null(lineITem1.Fields["unit_measure"].SimpleField.Value);

            var lineITem2 = lineItemsField.ObjectItems[2];
            Assert.NotNull(lineITem2.Fields);
            Assert.Equal(8, lineITem2.Fields.Count);
            Assert.Equal("Labor 3hrs", lineITem2.Fields["description"].SimpleField.Value);
            Assert.Equal(3, lineITem2.Fields["quantity"].SimpleField.Value);
            Assert.Equal(15, lineITem2.Fields["unit_price"].SimpleField.Value);
            Assert.Equal(45, lineITem2.Fields["total_price"].SimpleField.Value);
            Assert.Null(lineITem2.Fields["tax_rate"].SimpleField.Value);
            Assert.Null(lineITem2.Fields["tax_amount"].SimpleField.Value);
            Assert.Null(lineITem2.Fields["product_code"].SimpleField.Value);
            Assert.Null(lineITem2.Fields["unit_measure"].SimpleField.Value);
        }

        private static ExtractionRagAnnotationResponse GetResponse(string path)
        {
            var localResponse = new LocalResponse(
                File.ReadAllText(Constants.V2ProductDir + path));
            return localResponse.DeserializeResponse<ExtractionRagAnnotationResponse>();
        }
    }
}
