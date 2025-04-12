using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Parse.Tokens
{
    public class TranslationToken : SentenceToken
    {
        public string Original { get; set; }

        public TranslationToken(string translation, string original) : base(translation)
        {
            Original = original;
        }
    }
}
