using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public class Ocr
    {
        [JsonPropertyName("mvision-v1")]
        public MvisionV1 MvisionV1 { get; set; }
    }

    public class MvisionV1
    {
        [JsonPropertyName("pages")]
        public List<Page> Pages { get; set; }
    }

    public class Page
    {
        [JsonPropertyName("all_words")]
        public List<AllWord> AllWords { get; set; }
    }

    public class AllWord
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
