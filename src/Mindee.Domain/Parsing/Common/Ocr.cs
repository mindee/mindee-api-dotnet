using System.Collections.Generic;

namespace Mindee.Domain.Parsing.Common
{
    public class Ocr
    {
        public MvisionV1 MvisionV1 { get; set; }
    }

    public class MvisionV1
    {
        public List<Page> Pages { get; set; }
    }

    public class Page
    {
        public List<AllWord> AllWords { get; set; }
    }
    
    public class AllWord
    {
        public double Confidence { get; set; }

        public List<List<double>> Polygon { get; set; }

        public string Text { get; set; }
    }
}
