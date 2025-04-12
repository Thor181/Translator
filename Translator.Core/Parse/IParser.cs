using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Parse
{
    public interface IParser
    {
        public ParseMode Mode { get; set; }
        public string Source { get; set; }
        public string[] Parse();
    }
}
