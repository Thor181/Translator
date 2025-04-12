using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Models.Translate
{
    public record Result<T>(bool Success, string Message, T? Body) where T : IResponse;
}
