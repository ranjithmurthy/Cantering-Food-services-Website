//using System;
//using System.Collections.Generic;
//using System.Net;
//using HtmlAgilityPack;
//using NReadability;

//namespace DragonClassifier
//{
//    class Program
//    {
//        static void Main()
//        {
//            const string url = "http://www.engadget.com/2014/02/19/nokia-lumia-icon-review/";


//            //These evidences are used as training data for the Dragon Classigfier
//            var  positiveReviews = new Evidence("Positive", "Repository\\Positive.Evidence.csv");
//            var  negativeReviews = new Evidence("Negative", "Repository\\Negative.Evidence.csv");

//            var testData = "Simply wonderful service and sourroundings - a true delight!";
                
//                //"The food is okay but not like tourists and foreigners in general. I was having dinner with colleagues and we have been charged to the account on 10% tip under \"Auslage\". The waiter also confirmed that for the Germans tipping is free but with tourists load directly 10% of the account. You can only pay cash or by credit card";

         

//            var classifier = new Classifier(positiveReviews, negativeReviews);

//           IDictionary<string,double> scores = classifier.Classify(testData, DragonHelper.DragonHelper.ExcludeList);

//            Console.WriteLine("Positive Score for " + url + " - " + scores["Positive"]);


//            Console.ReadKey();
//        }

//        private static String GetWebpageContents(String url)
//        {
//            var nreadabilityTranscoder = new NReadabilityTranscoder();
//            using (var wc = new WebClient())
//            {
//                var rawHtml = wc.DownloadString(url);
//                var transcodingInput = new TranscodingInput(rawHtml);
//                var extractedHtml = nreadabilityTranscoder.Transcode(transcodingInput).ExtractedContent;
//                var pageHtml = new HtmlDocument();
//                pageHtml.LoadHtml(extractedHtml);
//                return pageHtml.DocumentNode.SelectSingleNode("//body").InnerText;
//            }
//        }
//    }
//}
