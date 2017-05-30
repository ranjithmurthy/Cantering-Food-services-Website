using System.Linq;

namespace DragonClassifier
{
    public static class DragonApiClass
    {
        private static Evidence _positiveReviews;

        private static Evidence _negativeReviews;

        public static Classifier Classifier;

        public static void LoadEvidence()
        {
            //These evidences are used as training data for the Dragon Classigfier
            _positiveReviews = new Evidence("Positive", "Repository\\Positive.Evidence.csv");
            _negativeReviews = new Evidence("Negative", "Repository\\Negative.Evidence.csv");

            Classifier = new Classifier(_positiveReviews, _negativeReviews);
        }

        public static string GetSentiment(string testData)
        {
            var scores = Classifier.Classify(testData, DragonHelper.DragonHelper.ExcludeList);

            //Console.WriteLine("Positive Score for " + url + " - " + scores["Positive"]);

            //Console.ReadKey();

            var ordered = scores.OrderByDescending(x => x.Value);

            var sentiment = ordered.FirstOrDefault().Key;

            return sentiment;
        }
    }
}