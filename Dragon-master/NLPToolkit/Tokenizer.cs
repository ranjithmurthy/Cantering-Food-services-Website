using java.io;
using opennlp.tools.tokenize;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using File = System.IO.File;
using FileNotFoundException = System.IO.FileNotFoundException;

namespace NLPToolkit
{
    /// <summary>
    ///     http://opennlp.apache.org/documentation/manual/opennlp.html#tools.tokenizer
    /// </summary>
    public static class Tokenizer
    {
        private static readonly FileInputStream ModelIn;
        private static readonly TokenizerModel Model;

        static Tokenizer()
        {
            //  var modelFile = ConfigurationManager.AppSettings["ModelTokenizer"] ?? string.Empty;

            var modelFile = AppDomain.CurrentDomain.BaseDirectory + "Repository\\en-token.bin";

            if (string.IsNullOrWhiteSpace(modelFile))
                throw new Exception("ModelTokenizer setting not defined in App.Config");

            modelFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, modelFile);
            if (!File.Exists(modelFile))
                throw new FileNotFoundException("Unable to find tokenizer model file at " + modelFile);

            ModelIn = new FileInputStream(modelFile);
            Model = new TokenizerModel(ModelIn);
        }

        /// <summary>
        ///     Split the input content to individual words
        /// </summary>
        /// <param name="contents">Content to split into words</param>
        /// <returns></returns>
        public static IEnumerable<string> TokenizeNow(string contents)
        {
            //ToDo: Make preprocessing function a functional pointer
            var processedContents = PreProcessing(contents);
            var tokenizer = new TokenizerME(Model);
            var tokens = tokenizer.tokenize(processedContents);
            return tokens;
        }

        /// <summary>
        ///     ToDo: Test scenario where numbers are replaced with *
        /// </summary>
        /// <param name="inp"></param>
        /// <returns></returns>
        private static string PreProcessing(string inp)
        {
            var sb = new StringBuilder();

            //Retain only characters
            foreach (var c in inp)
                if (c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                    sb.Append(c);
                else
                    sb.Append(' ');

            return sb.ToString();
        }
    }
}