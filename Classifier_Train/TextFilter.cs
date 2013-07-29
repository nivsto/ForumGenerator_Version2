using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Classifier_Train
{
    public class TextFilter
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


        public static bool isRelevantText(List<string> input, HashSet<string> vocabulary)
        {
            double prob = 0;
            double size = input.Count;
            if (size == 0)
                return true;

            double i = 0;
            foreach (string word in input)
            {
                if (vocabulary.Contains(word))
                    i++;
            }
            prob = i / size;
            Console.WriteLine("Match = " + prob);
            return (prob >= MIN_PROB);
        }


        public static HashSet<string> addWordsToHashSet(List<string> src, HashSet<string> dest)
        {
            foreach (string word in src)
            {
                dest.Add(word);
            }
            return dest;
        }


        public static HashSet<string> teachMatch(string text, HashSet<string> stopWords, HashSet<string> vocabulary)
        {
            List<string> input = TextFilter.removePanctuation(text);
            input = TextFilter.removeStopWords(input, stopWords);
            return TextFilter.teachMatch(input, vocabulary);
        }

        public static HashSet<string> teachMatch(List<string> text, HashSet<string> vocabulary)
        {
            return TextFilter.addWordsToHashSet(text, vocabulary);
        }




    }
}
