using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Translator.Core.Common;
using Translator.Core.Models.Utils;
using Translator.Core.Translate.Languages;

namespace Translator.Core.Translate.Api.Google
{
    public class GoogleTranslateApi : ITranslateApi
    {
        public int TextMaxLength => 1800;

        public IProgress<int>? Progress { get; set; }
        public Language SourceLanguage { get; set; } = Language.Auto;
        public required Language TargetLanguage { get; set; }

        public Result<IResponse> Translate(string text)
        {
            var escapedText = Uri.EscapeDataString(text);

            if (escapedText.Length > TextMaxLength)
                throw new InvalidOperationException($"{nameof(text)} max length should be less than {TextMaxLength} after transforming to escaped");

            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={SourceLanguage}&tl=${TargetLanguage}&dt=t&dt=bd&dj=1&q={escapedText}";

            var response = SingleHttpClient.Get(url);

            bool success = response.IsSuccessStatusCode;
            string message = success ? string.Empty : response.Content.ReadAsString();
            GoogleApiResponse? apiResponse = success ? response.Content.ReadFromJson<GoogleApiResponse>() : null;
            GoogleResponse? body = success && apiResponse != null ? new GoogleResponse(apiResponse) : null;

            return new Result<IResponse>(success, message, body);
        }

        public IEnumerable<Result<IResponse>> Translate(params IEnumerable<string> texts)
        {
            var index = 0;
            foreach (var text in texts)
            {
                var translation = Translate(text);
                yield return translation;
                Progress?.Report(++index);   
            }
        }
    }
}
