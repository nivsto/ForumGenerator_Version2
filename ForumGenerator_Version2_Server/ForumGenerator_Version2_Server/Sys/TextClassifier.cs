using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ForumGenerator_Version2_Server.Sys
{
    public class TextClassifier
    {

        public const double MIN_PROB = 0.5d;

        public static HashSet<string> getStopWords(string fileName)
        {
            HashSet<string> stopWords = new HashSet<string>();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines) { stopWords.Add(line); };
            return stopWords;
        }

        // Removes all panctuations and trims by spaces.
        public static List<string> removePanctuation(string text)
        {
            List<string> res = new List<string>();
            text = text.ToLower();
            text = Regex.Replace(text, @"[^\w\s\']", "");
            string[] words = text.Split(' ');
            foreach (string w in words)
            {
                res.Add(w);
            }
            return res;
        }


        public static List<string> removeStopWords(List<string> src, HashSet<string> stopWords)
        {
            List<string> keyWords = new List<string>();
            foreach (string word in src)
            {
                if (!stopWords.Contains(word)) { keyWords.Add(word); }
            }
            return keyWords;
        }


        public static bool isRelevantText(List<string> keyWords, HashSet<string> vocabulary)
        {
            double prob = 0;
            int size = keyWords.Count;
            int i = 0;
            foreach (string word in keyWords)
            {
                if (vocabulary.Contains(word))
                    i++;
            }
            prob = i / size;
            Console.WriteLine("Match = " + prob);
            return (prob >= MIN_PROB);
        }



        public static HashSet<string> addToVocabulary(List<string> words, HashSet<string> vocabulary)
        {
            foreach (string word in words)
            {
                vocabulary.Add(word);
            }
            return vocabulary;
        }




    }
}
