using Mindee.Input;
using Mindee.V2;
using Mindee.V2.Product.Extraction.RagDocuments;
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
                Constants.V2ProductDir + "extraction/financial_document/default_sample.jpg");
            var parameters = new RagDocumentUploadParameters(modelId: _extractionModelId);

            var postResponse = await _client.UploadAndGetRagDocumentPollAsync<RagAnnotationResponse>(
                inputSource, parameters);
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

            var patchAnnotationResponse = await _client.UpdateRagAnnotationAsync<RagAnnotationResponse>(
                new RagDocumentAnnotationParameters(
                    documentId: documentId
                    , annotation: postAnnotation));
            Assert.NotNull(patchAnnotationResponse);
            var patchAnnotation = patchAnnotationResponse.Annotation;
            Assert.Equal("I am the walrus!", patchAnnotation.Fields["supplier_name"].SimpleField.Guidelines);
            Assert.True(patchAnnotation.Fields["supplier_name"].SimpleField.Selected);
            Assert.Equal("koo koo katchoo!", patchAnnotation.Fields["invoice_number"].SimpleField.Guidelines);
            Assert.True(patchAnnotation.Fields["invoice_number"].SimpleField.Selected);

            var getResponse = await _client.GetReadyRagDocumentPollAsync<RagAnnotationResponse>(
                documentId);
            Assert.NotNull(getResponse);
            var getAnnotation = getResponse.Annotation;
            Assert.NotNull(getAnnotation);

            Assert.Equal("Draft", getResponse.Status);

            Assert.Equal("I am the walrus!", getAnnotation.Fields["supplier_name"].SimpleField.Guidelines);
            Assert.True(getAnnotation.Fields["supplier_name"].SimpleField.Selected);
            Assert.Equal("koo koo katchoo!", getAnnotation.Fields["invoice_number"].SimpleField.Guidelines);
            Assert.True(getAnnotation.Fields["invoice_number"].SimpleField.Selected);

            var patchStatusResponse = await _client.UpdateRagAnnotationAsync<RagAnnotationResponse>(
                new RagDocumentAnnotationParameters(
                    documentId: documentId
                    , status: "Active"));
            Assert.NotNull(patchStatusResponse);
            Assert.Equal("Active", patchStatusResponse.Status);

            var deleteResponse = await _client.DeleteExtractionRagDocumentAsync(documentId);
            Assert.True(deleteResponse);

            await Assert.ThrowsAsync<Mindee.V2.Exceptions.MindeeHttpExceptionV2>(async () =>
            {
                await _client.GetRagDocumentAsync<RagAnnotationResponse>(documentId);
            });
        }
    }
}
