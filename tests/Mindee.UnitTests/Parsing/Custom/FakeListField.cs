using Mindee.Geometry;
using Mindee.Parsing.Custom;

namespace Mindee.UnitTests.Parsing.Custom
{
    public static class FakeListField
    {
        public static Dictionary<string, ListField> GetWith2ValuesByExpectedLines()
        {
            var fakes = new Dictionary<string, ListField>
            {
                {
                    "birthDates",
                    new ListField(
                        1.0,
                        new List<ListFieldValue>()
                        {
                            new ListFieldValue(
                                "1986-10-23",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.818, 0.398),
                                        new Point(0.902, 0.398),
                                        new Point(0.902, 0.406),
                                        new Point(0.818, 0.406)
                                    }),
                                0
                                ),
                            new ListFieldValue(
                                "2012-02-13",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.819, 0.442),
                                        new Point(0.902, 0.442),
                                        new Point(0.902, 0.451),
                                        new Point(0.819, 0.451)
                                    }),
                                0
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
                                "Kevin",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.082, 0.398),
                                        new Point(0.144, 0.398),
                                        new Point(0.144, 0.407),
                                        new Point(0.082, 0.407)
                                    }),
                                0
                                ),
                            new ListFieldValue(
                                "Mindee",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.152, 0.398),
                                        new Point(0.222, 0.398),
                                        new Point(0.222, 0.407),
                                        new Point(0.152, 0.407)
                                    }),
                                0
                                ),
                            new ListFieldValue(
                                "Ianaré",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.081, 0.442),
                                        new Point(0.15, 0.442),
                                        new Point(0.15, 0.451),
                                        new Point(0.081, 0.451)
                                    }),
                                0
                                ),
                            new ListFieldValue(
                                "Mindee",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.157, 0.442),
                                        new Point(0.26, 0.442),
                                        new Point(0.26, 0.451),
                                        new Point(0.157, 0.451)
                                    }),
                                0
                                )
                        })
                }
            };

            return fakes;
        }

        public static Dictionary<string, ListField> GetWith1FieldValueForTheLastLine()
        {
            var fakes = new Dictionary<string, ListField>
            {
                {
                    "birthDates",
                    new ListField(
                        1.0,
                        new List<ListFieldValue>()
                        {
                            new ListFieldValue(
                                "1986-10-23",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.818, 0.398),
                                        new Point(0.902, 0.398),
                                        new Point(0.902, 0.406),
                                        new Point(0.818, 0.406)
                                    }),
                                0
                                ),
                            new ListFieldValue(
                                "2012-02-13",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.819, 0.442),
                                        new Point(0.902, 0.442),
                                        new Point(0.902, 0.451),
                                        new Point(0.819, 0.451)
                                    }),
                                0
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
                                "Kevin",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.082, 0.398),
                                        new Point(0.144, 0.398),
                                        new Point(0.144, 0.407),
                                        new Point(0.082, 0.407)
                                    }),
                                0
                                ),
                            new ListFieldValue(
                                "Mindee",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.152, 0.398),
                                        new Point(0.222, 0.398),
                                        new Point(0.222, 0.407),
                                        new Point(0.152, 0.407)
                                    }),
                                0
                                ),
                            new ListFieldValue(
                                "Ianaré",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.081, 0.442),
                                        new Point(0.15, 0.442),
                                        new Point(0.15, 0.451),
                                        new Point(0.081, 0.451)
                                    }),
                                0
                                ),
                            new ListFieldValue(
                                "Mindee",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.157, 0.442),
                                        new Point(0.26, 0.442),
                                        new Point(0.26, 0.451),
                                        new Point(0.157, 0.451)
                                    }),
                                0
                                ),
                            new ListFieldValue(
                                "Bob",
                                1.0,
                                new Polygon(
                                    new List<Point>()
                                    {
                                        new Point(0.082, 0.486),
                                        new Point(0.151, 0.486),
                                        new Point(0.151, 0.495),
                                        new Point(0.082, 0.495)
                                    }),
                                0
                                )
                        })
                }
            };

            return fakes;
        }

        public static Dictionary<string, ListField> GetWith1ExpectedLines()
        {
            var fakes = new Dictionary<string, ListField>
            {
                {
                    "birthDates",
                    new ListField(
                    1.0,
                    new List<ListFieldValue>()
                    {
                        new ListFieldValue(
                            "1986-10-23",
                            1.0,
                            new Polygon(
                                new List<Point>()
                                {
                                        new Point(0.818, 0.398),
                                        new Point(0.902, 0.398),
                                        new Point(0.902, 0.406),
                                        new Point(0.818, 0.406)
                                }),
                            0
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
                            "Kevin",
                            1.0,
                            new Polygon(
                                new List<Point>()
                                {
                                    new Point(0.082, 0.398),
                                    new Point(0.144, 0.398),
                                    new Point(0.144, 0.407),
                                    new Point(0.082, 0.407)
                                }),
                            0
                            ),
                        new ListFieldValue(
                            "Mindee",
                            1.0,
                            new Polygon(
                                new List<Point>()
                                {
                                    new Point(0.152, 0.398),
                                    new Point(0.222, 0.398),
                                    new Point(0.222, 0.407),
                                    new Point(0.152, 0.407)
                                }),
                            0
                            )
                    })
                }
            };

            return fakes;
        }

        public static Dictionary<string, ListField> GetWithPolygonsNotExactlyOnTheSameAxis()
        {
            var fakes = new Dictionary<string, ListField>();
            fakes.Add(
              "birthDates",
              new ListField(
                1.0,
                        new List<ListFieldValue>()
                        {
                    new ListFieldValue(
                        "1986-10-23",
                        1.0,
                        new Polygon(
                            new List<Point>()
                            {
                                new Point(0.576, 0.401),
                                new Point(0.649, 0.401),
                                new Point(0.649, 0.408),
                                new Point(0.576, 0.408)
                                }),
                        0
                            ),
                    new ListFieldValue(
                        "2012-02-13",
                        1.0,
                        new Polygon(
                            new List<Point>()
                            {
                                new Point(0.581, 0.45),
                                new Point(0.656, 0.45),
                                new Point(0.656, 0.458),
                                new Point(0.581, 0.458)
                                }),
                        0
                            )
                        })
            );

            fakes.Add(
                "names",
                new ListField(
                    1.0,
                    new List<ListFieldValue>()
                    {
                        new ListFieldValue(
                            "Kevin",
                            1.0,
                            new Polygon(
                                new List<Point>()
                                {
                                    new Point(0.119, 0.4),
                                    new Point(0.179, 0.4),
                                    new Point(0.178, 0.41),
                                    new Point(0.119, 0.409)
                                    }),
                            0
                                ),
                        new ListFieldValue(
                            "Mindee",
                            1.0,
                            new Polygon(
                                new List<Point>()
                                {
                                    new Point(0.185, 0.401),
                                    new Point(0.232, 0.401),
                                    new Point(0.232, 0.41),
                                    new Point(0.184, 0.409)
                                    }),
                            0
                                ),
                        new ListFieldValue(
                            "Ianaré",
                            1.0,
                            new Polygon(
                                new List<Point>()
                                {
                                    new Point(0.118, 0.45),
                                    new Point(0.169, 0.451),
                                    new Point(0.169, 0.458),
                                    new Point(0.117, 0.457)
                                    }),
                            0
                                )
                    })
            );

            return fakes;
        }

        public static Dictionary<string, ListField> GetSampleWichRender2LinesInsteadOfOne()
        {
            var fakes = new Dictionary<string, ListField>();

            fakes.Add(
              "names",
              new ListField(
                  1.0,
                  new List<ListFieldValue>()
                  {
                      new ListFieldValue(
                        "A",
                        1.0,
                        new Polygon(
                            new List<Point>()
                            {
                                new Point(0.075, 0.42),
                                new Point(0.141, 0.42),
                                new Point(0.141, 0.428),
                                new Point(0.075, 0.428)
                            }),
                        0
                        ),
                    new ListFieldValue(
                        "B",
                      1.0,
                        new Polygon(
                            new List<Point>()
                            {
                                new Point(0.148, 0.42),
                                new Point(0.198, 0.42),
                                new Point(0.198, 0.428),
                                new Point(0.148, 0.428)
                                }),
                        0
                        ),
                    new ListFieldValue(
                        "C",
                      1.0,
                        new Polygon(
                            new List<Point>()
                            {
                                new Point(0.2, 0.42),
                                new Point(0.204, 0.42),
                                new Point(0.204, 0.428),
                                new Point(0.2, 0.428)
                            }),
                        0
                        ),
                    new ListFieldValue(
                        "D",
                      1.0,
                        new Polygon(
                            new List<Point>()
                            {
                                new Point(0.206, 0.42),
                                new Point(0.257, 0.42),
                                new Point(0.257, 0.428),
                                new Point(0.206, 0.428)
                            }),
                        0
                        ),
                    new ListFieldValue(
                        "E",
                      1.0,
                        new Polygon(
                            new List<Point>()
                            {
                                new Point(0.263, 0.42),
                                new Point(0.33, 0.42),
                                new Point(0.33, 0.428),
                                new Point(0.263, 0.428)
                            }),
                        0
                        )
                  })
            );

            return fakes;
        }
    }
}
