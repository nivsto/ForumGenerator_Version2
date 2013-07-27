using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using NClassifier.Bayesian;
using NClassifier;


namespace Classifier_Train
{
    public class Trainer
    {
        public SimpleWordsDataSource wds;
        public BayesianClassifier bc;

        public Trainer()
        {
            wds = new SimpleWordsDataSource();
            bc = new BayesianClassifier(wds, new DefaultTokenizer());
        }


        public List<string> readMsgsFile(string FileName)
        {
            List<string> msgs = new List<string>();
            string[] lines = File.ReadAllLines(FileName);
            foreach (string line in lines) { msgs.Add(line); };
            return msgs;
        }


        public void teachNonMatch(string input)
        {
            bc.TeachNonMatch(ICategorizedClassifierConstants.DEFAULT_CATEGORY, input);
        }

        public void trainMatch(string fileName)
        {
            List<string> msgs = readMsgsFile(fileName);
            foreach (string msg in msgs)
            {
                bc.TeachMatch(ICategorizedClassifierConstants.DEFAULT_CATEGORY, msg);
            }
        }


        public void trainNonMatch(string fileName)
        {
            List<string> msgs = readMsgsFile(fileName);
            foreach (string msg in msgs)
            {
                bc.TeachNonMatch(ICategorizedClassifierConstants.DEFAULT_CATEGORY, msg);
            }
        }

        public bool classify(string text)
        {
            double res = bc.Classify(ICategorizedClassifierConstants.DEFAULT_CATEGORY, text);
            Console.WriteLine("Match = " + res);          
            Console.ReadKey();
            return (res >= 0.7d);
        }

    }
}
