﻿using Mindee.Parsing;
using Mindee.Parsing.Cropper;

namespace Mindee.UnitTests.Parsing.Receipt
{
    public class CropperV1Test
    {
        [Fact]
        [Trait("Category", "Cropper V1")]
        public async Task Predict_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CropperV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(prediction);
        }

        [Fact]
        [Trait("Category", "Cropper V1")]
        public async Task Predict_WithCropping_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<CropperV1Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(prediction.Inference.Pages.First().Prediction.Cropping);
            Assert.Equal(2, prediction.Inference.Pages.First().Prediction.Cropping.Count);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.588, 0.25 },
                new List<double>() { 0.953, 0.25 },
                new List<double>() { 0.953, 0.682 },
                new List<double>() { 0.588, 0.682 },
            }
            , prediction.Inference.Pages.First().Prediction.Cropping.First().BoundingBox);

            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.589, 0.252 },
                new List<double>() { 0.953, 0.25 },
                new List<double>() { 0.949, 0.639 },
                new List<double>() { 0.607, 0.681 },
            }
            , prediction.Inference.Pages.First().Prediction.Cropping.First().Quadrangle);

            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.588, 0.25 },
                new List<double>() { 0.951, 0.25 },
                new List<double>() { 0.951, 0.68 },
                new List<double>() { 0.588, 0.68 },
            }
            , prediction.Inference.Pages.First().Prediction.Cropping.First().Rectangle);

            Assert.Equal(
                new List<double>() { 0.598, 0.377 }
            , prediction.Inference.Pages.First().Prediction.Cropping.First().Polygon.First());
        }

        private MindeeApi GetMindeeApiForReceipt(string fileName = "Resources/cropper/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}