using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

using ForumGenerator_Version2_Server.ForumData;

namespace ForumGenerator_Version2_Server.Sys
{
    public class TextClassifier
    {

        public const double MIN_PROB = 0.25d;

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
                if(w != "")
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


        public static bool isRelevantText(List<string> keyWords, HashSet<Word> vocabulary)
        {
            double prob = 0;
            double size = keyWords.Count;
            if (size == 0)
                return true;

            double i = 0;
            foreach (string word in keyWords)
            {
                Word w = new Word(word);
                if (vocabulary.Contains(w))
                    i++;
            }
            prob = i / size;
            Console.WriteLine("Match = " + prob);
            return (prob >= MIN_PROB);
        }



        public static HashSet<Word> addToVocabulary(List<string> words, HashSet<Word> vocabulary)
        {
            foreach (string word in words)
            {
                Word w = new Word(word);
                if (!vocabulary.Contains(w))
                    vocabulary.Add(w);
            }
            return vocabulary;
        }




    }
}
