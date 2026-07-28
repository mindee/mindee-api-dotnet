using Mindee.Input;
using Mindee.V2;
using Mindee.V2.Product.Extraction.RagDocuments.Params;

namespace Mindee.IntegrationTests.V2.Product.Extraction
{
    [Trait("Category", "V2")]
    [Trait("Category", "Integration")]
    public class RagDocumentsTest
    {
        private readonly string? _extractionModelId;
        private readonly Client _client;

        public RagDocumentsTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _extractionModelId = Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id");
        }

        [Fact(Timeout = 180000)]
        public async Task RagDocument_Lifecycle_MustSucceed()
        {
            var inputSource = new LocalInputSource(
                Constants.RootDir + "file_types/pdf/blank_1.pdf");
            var parameters = new RagDocumentUploadParameters(modelId: _extractionModelId);

            var postResponse = await _client.UploadAndGetExtractionRagDocumentAsync(inputSource, parameters);
            Assert.NotNull(postResponse);

            var postAnnotation = postResponse.Annotation;
            Assert.NotNull(postAnnotation.Fields);

            var documentId = postResponse.Id;
            Assert.NotNull(documentId);

            Assert.Equal("Draft", postResponse.Status);

            postAnnotation.Fields["supplier_name"].SimpleField.Selected = true;
            postAnnotation.Fields["supplier_name"].SimpleField.Guidelines = "I am the walrus!";
            postAnnotation.Fields["invoice_number"].SimpleField.Selected = true;
            postAnnotation.Fields["invoice_number"].SimpleField.Guidelines = "koo koo katchoo!";

            var patchResponse = await _client.UpdateExtractionRagAnnotationAsync(
                new RagDocumentAnnotationParameters(documentId: documentId, annotation: postAnnotation));
            Assert.NotNull(patchResponse);
            var patchAnnotation = patchResponse.Annotation;
            Assert.NotNull(patchResponse);
            Assert.Equal("I am the walrus!", patchAnnotation.Fields["supplier_name"].SimpleField.Guidelines);
            Assert.True(patchAnnotation.Fields["supplier_name"].SimpleField.Selected);
            Assert.Equal("koo koo katchoo!", patchAnnotation.Fields["invoice_number"].SimpleField.Guidelines);
            Assert.True(patchAnnotation.Fields["invoice_number"].SimpleField.Selected);

            var deleteResponse = await _client.DeleteExtractionRagDocumentAsync(documentId);
            Assert.True(deleteResponse);

            await Assert.ThrowsAsync<Mindee.V2.Exceptions.MindeeHttpExceptionV2>(async () =>
            {
                await _client.GetExtractionRagDocumentAsync(documentId);
            });
        }
    }
}
