using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Translate.Languages
{
    public record Language(string Name)
    {
        public static readonly Language English = new Language("en");
        public static readonly Language Russian = new Language("ru");
        public static readonly Language Auto = new Language("auto");

        public override string ToString()
        {
            return Name;
        }
    }
}
