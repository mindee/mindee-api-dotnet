using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Ocr result information.
    /// </summary>
    public class Ocr
    {
        /// <summary>
        /// <see cref="Common.MvisionV1"/>
        /// </summary>
        [JsonPropertyName("mvision-v1")]
        public MvisionV1 MvisionV1 { get; set; }

        /// <summary>
        /// The OCR as a single string.
        /// </summary>
        public override string ToString()
        {
            return MvisionV1.ToString();
        }
    }

    /// <summary>
    /// List all pages that have ocr results.
    /// </summary>
    public class MvisionV1
    {
        /// <summary>
        /// All the ocr pages.
        /// </summary>
        [JsonPropertyName("pages")]
        public List<Page> Pages { get; set; }

        /// <summary>
        /// The Mvision OCR as a single string.
        /// </summary>
        public override string ToString()
        {
            return string.Join("\n", Pages);
        }
    }

    /// <summary>
    /// Represent a page.
    /// </summary>
    public class Page
    {
        /// <summary>
        /// List of words found.
        /// </summary>
        [JsonPropertyName("all_words")]
        public List<Word> AllWords { get; set; }

        private List<List<Word>> _allLines;

        /// <summary>
        /// All the words on the page, ordered in lines.
        /// </summary>
        public List<List<Word>> AllLines
        {
            get
            {
                if (_allLines == null)
                    _allLines = ToLines();
                return _allLines;
            }
            private set { }
        }

        /// <summary>
        /// Represent a page.
        /// </summary>
        [JsonConstructor]
        public Page(List<Word> allWords)
        {
            AllWords = allWords;

            // make sure words are sorted from top to bottom
            AllWords.Sort(
                ((word1, word2) => Utils.CompareOnY(word1.Polygon, word2.Polygon)));
        }

        /// <summary>
        /// The page OCR as a single string.
        /// </summary>
        public override string ToString()
        {
            var outStr = new StringBuilder();
            foreach (var line in AllLines)
            {
                var words = line.Select(word => word.ToString()).ToArray();
                outStr.Append(string.Join(" ", words) + "\n");
            }
            return outStr.ToString();
        }

        /// <summary>
        /// Order all the words on the page into lines.
        /// </summary>
        protected List<List<Word>> ToLines()
        {
            Word current = null;
            var indexes = new List<int>();
            var lines = new List<List<Word>>();

            // go through each word ...
            foreach (var ignored in AllWords)
            {
                var line = new List<Word>();
                int idx = 0;
                // ... and compare it to all other words.
                foreach (var word in AllWords)
                {
                    idx += 1;
                    if (indexes.Contains(idx))
                        continue;
                    if (current == null)
                    {
                        current = word;
                        indexes.Add(idx);
                        line = new List<Word>();
                        line.Add(word);
                    }
                    else if (AreWordsOnSameLine(current, word))
                    {
                        line.Add(word);
                        indexes.Add(idx);
                    }
                }
                current = null;
                if (line.Count > 0)
                {
                    line.Sort(
                        ((word1, word2) => Utils.CompareOnX(word1.Polygon, word2.Polygon)));
                    lines.Add(line);
                }
            }
            return lines;
        }

        private bool AreWordsOnSameLine(Word currentWord, Word nextWord)
        {
            bool currentInNext = currentWord.Polygon.IsPointInY(nextWord.Polygon.GetCentroid());
            bool nextInCurrent = nextWord.Polygon.IsPointInY(currentWord.Polygon.GetCentroid());
            // We need to check both to eliminate any issues due to word order.
            return currentInNext || nextInCurrent;
        }
    }

    /// <summary>
    /// Represent a word.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }

        /// <summary>
        /// Define the coordinates of the zone in the page where the values has been found.
        /// </summary>
        [JsonPropertyName("polygon")]
        [JsonConverter(typeof(PolygonJsonConverter))]
        public Polygon Polygon { get; set; }

        /// <summary>
        /// Represent the content.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// The word OCR as a string.
        /// </summary>
        public override string ToString()
        {
            return Text;
        }
    }
}
