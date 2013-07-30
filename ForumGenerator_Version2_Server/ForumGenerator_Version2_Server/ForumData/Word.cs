using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ForumGenerator_Version2_Server.ForumData
{
    public class Word
    {
        [Key]
        public string word { get; private set; }

        public Word(string word)
        {
            this.word = word;
        }

    }
}
