using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Parse.Tokens
{
    public abstract class BaseToken
    {
        public string Content { get; }
        public TokenType Type { get; }

        public BaseToken(string content, TokenType tokenType)
        {
            Content = content;
            Type = tokenType;
        }
    }
}
