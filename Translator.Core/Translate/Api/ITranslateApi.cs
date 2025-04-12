using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Core.Translate.Languages;
using Translator.Core.Translate.Api.Google.Models;
using Translator.Core.Models.Utils;
using Translator.Core.Models.Translate;
using Translator.Core.Parse.Tokens;

namespace Translator.Core.Translate.Api
{
    public interface ITranslateApi
    {
        public int TextMaxLength { get; }
        public IProgress<int>? Progress { get; set; }
        public Language SourceLanguage { get; set; }
        public Language TargetLanguage { get; set; }
        public Result<IResponse> Translate(string text);
        public IEnumerable<Result<IResponse>> Translate(params IEnumerable<string> texts);
        public IEnumerable<TranslationToken> Translate(params IEnumerable<SentenceToken> sentences);


    }
}
