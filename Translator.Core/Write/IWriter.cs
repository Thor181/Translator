using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Core.Models.Translate;

namespace Translator.Core.Write
{
    public interface IWriter
    {
        public string Path { get; set; }
        public bool AddOriginal { get; set; }
        public void Write(params IEnumerable<IResponse> texts);
    }
}
