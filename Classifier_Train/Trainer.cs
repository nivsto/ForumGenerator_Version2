using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NClassifier.Bayesian;


namespace Classifier_Train
{
    public class Trainer
    {

        public Trainer() { }

        public void classify(string text)
        {
            IWordsDataSource wds = new SimpleWordsDataSource();
            BayesianClassifier classifier = new BayesianClassifier(wds);
          
            Console.ReadKey();

        }

    }
}
