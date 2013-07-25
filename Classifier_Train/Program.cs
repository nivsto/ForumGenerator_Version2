using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NClassifier.Bayesian;

namespace Classifier_Train
{
    class Program
    {
        static void Main(string[] args)
        {
            Trainer tr = new Trainer();

            string text = "The phrases are more complicated. You could try to " +
                          "split the text into phrases first and then apply the " +
                          "keyword search on these phrases instead of searching " +
                          "the keywords in the whole text. This would give you " +
                          "the number of keywords in a phrase at the same time." +
                          " asa doron niv irit gideon ofer mira uri avraham";
            tr.classify(text);
        }
    }
}
