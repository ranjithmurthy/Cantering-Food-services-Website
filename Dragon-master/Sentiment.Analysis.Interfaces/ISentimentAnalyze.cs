using System;

namespace Sentiment.Analysis.Interfaces
{
    public class Score
    {
        public double PositiveScore { get; set; }
        public double NegativeScore { get; set; }
        public string AnalyzedBy { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    internal interface ISentimentAnalyze
    {
        Score DoSentimentAnalysis(string contents);

        Score DoSentimentAnalysis(Uri url);
    }
}