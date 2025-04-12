using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Parse.Tokens
{
    public class ParagraphToken : BaseToken
    {
        public ParagraphToken() : base(Environment.NewLine, TokenType.Paragraph)
        {
            
        }
    }
}
