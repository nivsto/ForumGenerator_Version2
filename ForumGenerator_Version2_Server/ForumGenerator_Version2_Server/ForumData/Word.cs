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
        public string word { get; set; }


        public Word()
        {
            this.word = "";
        }

        public Word(string word)
        {
            this.word = word;
        }


        public override bool Equals(object other)
        {
            var obj = other as Word;
            if (obj == null)
                return false;

            return this.word == ((Word)other).word;
        }

        public override int GetHashCode()
        {
            return this.word.GetHashCode();
        }

    }
}
