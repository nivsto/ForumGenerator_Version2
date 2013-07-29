using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier_Train
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HashSet<string> stopWords = TextFilter.getStopWords("DefaultStopWords.txt");
            HashSet<string> vocabulary = new HashSet<string>();

            string[] text = {"I am Asa and I'm a student in BGU, means I am a SLAVE!",
                             "Niv is also a fucking slave. He works in Intel with Doron...",
                             "class lectures are suck. also all teaching assignments.",
                             "When can we finally go home? University is suck. real suck",
                             "this is now the exams peroid - WOW"};
            foreach (string line in text)
            {
                vocabulary = teachMatch(line, stopWords, vocabulary);
            }
            List<string> input = new List<string>();
            string text1 = "I'm going to be a STUDENT next year!";
            string text2 = "Me too";

            input = TextFilter.removePanctuation(text1);
            input = TextFilter.removeStopWords(input, stopWords);
            TextFilter.isRelevantText(input, vocabulary);
            input = TextFilter.removePanctuation(text2);
            input = TextFilter.removeStopWords(input, stopWords);
            TextFilter.isRelevantText(input, vocabulary);
            Console.ReadKey();

        }

        public static HashSet<string> teachMatch(string text, HashSet<string> stopWords, HashSet<string> vocabulary)
        {
            List<string> keyWords = TextFilter.removePanctuation(text);
            keyWords = TextFilter.removeStopWords(keyWords, stopWords);
            vocabulary = TextFilter.addWordsToHashSet(keyWords, vocabulary);
            return vocabulary;
        }

    }
}
