using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Parse.Tokens
{
    public class SentenceToken : BaseToken
    {
        public SentenceToken(string sentence) : base(sentence, TokenType.Sentence)
        {

        }
    }
}
