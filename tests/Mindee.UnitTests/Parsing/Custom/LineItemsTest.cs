using Mindee.Geometry;
using Mindee.Parsing.Custom;
using Mindee.Parsing.Custom.LineItem;

namespace Mindee.UnitTests.Parsing.Custom
{
    [Trait("Category", "Custom API - Line items")]
    public class LineItemsTest
    {
        [Fact]
        public void PrepareLines_With2ValuesOnEachLine_WithPolygonValuesOnExactlyTheSameAxis()
        {
            Anchor anchor = new Anchor("names");

            IEnumerable<Line> table = LineItemsGenerator.GetPreparedLines(
                FakeListField.GetWith2ValuesByExpectedLines(),
                anchor);

            Assert.Equal(2, table.Count());
        }

        [Fact]
        public void PrepareLines_With1FieldValueForTheLastLine()
        {
            Anchor anchor = new Anchor("names");

            var table = LineItemsGenerator.GetPreparedLines(
                FakeListField.GetWith1FieldValueForTheLastLine(),
                anchor);

            Assert.Equal(3, table.Count());
        }

        [Fact]
        public void PrepareLines_With1ExpectedLine()
        {
            Anchor anchor = new Anchor("names");

            var table = LineItemsGenerator.GetPreparedLines(
                FakeListField.GetWith1ExpectedLines(),
                anchor);

            Assert.Single(table);
        }

        [Fact]
        public void PrepareLines_WithPolygonsNotExactlyOnTheSameAxis()
        {
            Anchor anchor = new Anchor("names", 0.005d);

            var table = LineItemsGenerator.GetPreparedLines(
                FakeListField.GetWithPolygonsNotExactlyOnTheSameAxis(),
                anchor);

            Assert.Equal(2, table.Count());
        }

        [Fact]
        public void PrepareLines_WhichRender2LinesInsteadOfOne()
        {
            Anchor anchor = new Anchor("names", 0.0d);

            var table = LineItemsGenerator.GetPreparedLines(
                FakeListField.GetSampleWichRender2LinesInsteadOfOne(),
                anchor);

            Assert.Single(table);
        }

        [Fact]
        public void Generate_WithFieldsToConvertTo2LinesItems_MustGetOnly2Lines()
        {
            // given
            var fakes = new Dictionary<string, ListField>
            {
                {
                    "birthDates",
                    new ListField(
                    1.0,
                    new List<ListFieldValue>()
                    {
                        new ListFieldValue(
                            "01/01/1990",
                            1.0,
                            new Polygon(new List<Point>()
                            {
                                new Point(0.764, 0.351),
                                new Point(0.846, 0.351),
                                new Point(0.846, 0.36),
                                new Point(0.764, 0.36)
                            })
                        ),
                        new ListFieldValue(
                            "01/01/20",
                            0.6,
                            new Polygon(new List<Point>()
                            {
                                new Point(0.765, 0.387),
                                new Point(0.847, 0.387),
                                new Point(0.847, 0.396),
                                new Point(0.766, 0.396)
                            })
                        )
                    })
                },
                {
                    "names",
                    new ListField(
                    1.0,
                    new List<ListFieldValue>()
                    {
                        new ListFieldValue(
                            "Chez Mindee",
                            1.0,
                            new Polygon(new List<Point>()
                            {
                                new Point(0.059, 0.351),
                                new Point(0.129, 0.351),
                                new Point(0.129, 0.36),
                                new Point(0.059, 0.36)
                            })),
                        new ListFieldValue(
                            "Kevin",
                            1.0,
                            new Polygon(new List<Point>()
                            {
                                new Point(0.136, 0.351),
                                new Point(0.224, 0.351),
                                new Point(0.224, 0.36),
                                new Point(0.136, 0.36)
                            })),
                        new ListFieldValue(
                            "Mindee",
                            1.0,
                            new Polygon(new List<Point>()
                            {
                                new Point(0.059, 0.388),
                                new Point(0.129, 0.388),
                                new Point(0.129, 0.397),
                                new Point(0.059, 0.397)
                            })),
                        new ListFieldValue(
                            "Ianare",
                            1.0,
                            new Polygon(new List<Point>()
                            {
                                new Point(0.136, 0.388),
                                new Point(0.189, 0.388),
                                new Point(0.189, 0.397),
                                new Point(0.136, 0.397)
                            }))
                    })
                }
            };

            // then
            LineItems lineItems = LineItemsGenerator.Generate(
                new Anchor("birthDates"),
                new List<string>() { "names", "birthDates" },
                fakes);

            Assert.NotNull(lineItems);
            Assert.Equal(2, lineItems.Lines.Count());
            Assert.Equal("Chez Mindee Kevin",
                lineItems.Lines.First().Fields["names"].Content);
            Assert.Equal("01/01/1990",
                lineItems.Lines.First().Fields["birthDates"].Content);
            Assert.Equal("Mindee Ianare",
                lineItems.Lines.Last().Fields["names"].Content);
            Assert.Equal("01/01/20",
                lineItems.Lines.Last().Fields["birthDates"].Content);
            Assert.Equal(0.6,
              lineItems.Lines.Last().Fields["birthDates"].Confidence);
        }
    }
}
