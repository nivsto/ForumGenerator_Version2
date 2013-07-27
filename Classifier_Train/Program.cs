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
            tr.trainMatch("goodMsgs.txt");
      //      tr.trainNonMatch("badMsgs.txt");
            string text1 = "I am talking about algorithm c++";
            tr.teachNonMatch("cooking and football");
            string text2 = "I am talking about cooking and football. also about sport";
            string text3 = "A wounded protester says he saw plain clothes officers open fire on pro-Morsy sport balls demonstrators as rival rallies stretched into the early hours of Saturday.";
            string text4 = "Yes";

            tr.classify(text1);
            tr.classify(text2);
            tr.classify(text3);
            tr.classify(text4);
        }
    }
}
