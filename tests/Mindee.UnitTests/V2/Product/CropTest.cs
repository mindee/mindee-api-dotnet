using System.Reflection;
using Mindee.Geometry;
using Mindee.V2.Parsing;
using Mindee.V2.Product;
using Mindee.V2.Product.Crop;
using Mindee.V2.Product.Crop.Params;

namespace Mindee.UnitTests.V2.Product
{
    [Trait("Category", "V2")]
    [Trait("Category", "CropInference")]
    public class CropTest
    {
        [Fact]
        public void Parameters_MustInit()
        {
            var productParams = new CropParameters("invalid-model-id");
            Assert.Equal("invalid-model-id", productParams.ModelId);

            var productAttributes = productParams.GetType().GetCustomAttribute<ProductAttributes>();
            Assert.Equal("crop", productAttributes?.Slug);
        }

        [Fact]
        public void Crop_WhenSingle_MustHaveValidProperties()
        {
            var response = GetInference("crop/crop_single.json");
            AssertInferenceResponse(response);

            var inference = response.Inference;

            Assert.Equal("12345678-1234-1234-1234-123456789abc", inference.Id);
            Assert.Equal("test-model-id", inference.Model.Id);
            Assert.Equal("12345678-1234-1234-1234-jobid1234567", inference.Job.Id);

            Assert.Equal("sample.jpeg", inference.File.Name);
            Assert.Equal(1, inference.File.PageCount);
            Assert.Equal("image/jpeg", inference.File.MimeType);

            var crops = inference.Result.Crops;
            Assert.NotNull(crops);
            Assert.Single(crops);

            var firstCrop = crops.First();
            Assert.Equal("invoice", firstCrop.ObjectType);
            Assert.Equal(0, firstCrop.Location.Page);

            var polygon = firstCrop.Location.Polygon;
            Assert.Equal(4, polygon.Count);
            Assert.Equal(new Point(0.15, 0.254), polygon[0]);
            Assert.Equal(new Point(0.85, 0.254), polygon[1]);
            Assert.Equal(new Point(0.85, 0.947), polygon[2]);
            Assert.Equal(new Point(0.15, 0.947), polygon[3]);
        }

        [Fact]
        public void Crop_WhenMultiple_MustHaveValidProperties()
        {
            var response = GetInference("crop/crop_multiple.json");
            AssertInferenceResponse(response);

            var inference = response.Inference;

            var job = inference.Job;
            Assert.Equal("12345678-1234-1234-1234-jobid1234567", job.Id);

            Assert.Equal("12345678-1234-1234-1234-123456789abc", inference.Id);
            Assert.Equal("test-model-id", inference.Model.Id);

            Assert.Equal("default_sample.jpg", inference.File.Name);
            Assert.Equal(1, inference.File.PageCount);
            Assert.Equal("image/jpeg", inference.File.MimeType);

            var crops = inference.Result.Crops;
            Assert.NotNull(crops);
            Assert.Equal(2, crops.Count);

            var firstCrop = crops[0];
            Assert.Equal("invoice", firstCrop.ObjectType);
            Assert.Equal(0, firstCrop.Location.Page);

            var firstPolygon = firstCrop.Location.Polygon;
            Assert.Equal(4, firstPolygon.Count);
            Assert.Equal(new Point(0.214, 0.079), firstPolygon[0]);
            Assert.Equal(new Point(0.476, 0.079), firstPolygon[1]);
            Assert.Equal(new Point(0.476, 0.979), firstPolygon[2]);
            Assert.Equal(new Point(0.214, 0.979), firstPolygon[3]);

            var secondCrop = crops[1];
            Assert.Equal("receipt", secondCrop.ObjectType);
            Assert.Equal(0, secondCrop.Location.Page);

            var secondPolygon = secondCrop.Location.Polygon;
            Assert.Equal(4, secondPolygon.Count);
            Assert.Equal(new Point(0.547, 0.15), secondPolygon[0]);
            Assert.Equal(new Point(0.862, 0.15), secondPolygon[1]);
            Assert.Equal(new Point(0.862, 0.97), secondPolygon[2]);
            Assert.Equal(new Point(0.547, 0.97), secondPolygon[3]);
        }

        [Fact(DisplayName = "crop_single.rst – RST display must be parsed and exposed")]
        public void RstDisplay_MustBeAccessible()
        {
            var resp = GetInference("crop/crop_single.json");
            var rstReference = File.ReadAllText(
                Constants.V2ProductDir + "crop/crop_single.rst");

            var inf = resp.Inference;

            Assert.NotNull(inf);

            Assert.Equal(
                NormalizeLineEndings(rstReference),
                NormalizeLineEndings(inf.ToString())
            );
        }

        /// <summary>
        ///     Ensures all line endings are identical before comparison so the test
        ///     behaves the same on every platform (LF vs CRLF).
        /// </summary>
        private static string NormalizeLineEndings(string input)
        {
            return input.Replace("\r\n", "\n").Replace("\r", "\n");
        }

        private static CropResponse GetInference(string path)
        {
            var localResponse = new LocalResponse(
                File.ReadAllText(Constants.V2ProductDir + path));
            return localResponse.DeserializeResponse<CropResponse>();
        }

        private void AssertInferenceResponse(CropResponse response)
        {
            Assert.NotNull(response.Inference);
            Assert.NotNull(response.Inference.Id);
            Assert.NotNull(response.Inference.File);
            Assert.NotNull(response.Inference.Result);
        }
    }
}
