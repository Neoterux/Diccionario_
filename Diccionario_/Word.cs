using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_
{
    public class Word
    {
        public Word(string wordname, string definition, string sinonyms, string antonyms, string other)
        {
            this.wordName = wordname;
            this.Definition = definition;
            this.Sinonyms = sinonyms;
            this.Antonyms = antonyms;
            this.Other = other;
        }
        public Word()
        {

        }
       public string wordName { get; set; }
       public string Definition { get; set; }
        public string Sinonyms { get; set; }
        public string Antonyms { get; set; }
        public string Other { get; set; }
        public string Sentence { get; set; }


    }
}
