using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Core.Models.Translate;

namespace Translator.Core.Models.Utils
{
    public record Result<T>(bool Success, string Message, T? Body) where T : IResponse;
}
