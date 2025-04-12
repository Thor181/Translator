using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Models.Utils
{
    public static class Extensions
    {
        public static string JoinString(this IEnumerable<string> strings, string separator = "")
        {
            return string.Join(separator, strings);
        }
    }
}
