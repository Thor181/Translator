using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Models.Translate
{
    public interface IResponse
    {
        public string Translation { get; }
        public string Original { get; }
    }
}
