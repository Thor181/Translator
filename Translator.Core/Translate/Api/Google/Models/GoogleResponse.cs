using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Translator.Core.Models.Translate;

namespace Translator.Core.Translate.Api.Google.Models
{
    public class GoogleResponse : IResponse
    {
        internal GoogleApiResponse Response { get; set; }

        public string Translation => Response.Sentences[0].Translation;
        public string Original => Response.Sentences[0].Original;

        internal GoogleResponse(GoogleApiResponse response)
        {
            Response = response;
        }
    }

    internal class GoogleApiResponse
    {
        public Sentence[] Sentences { get; set; }
    }

    internal class Sentence
    {
        [JsonPropertyName("trans")]
        public string Translation { get; set; }

        [JsonPropertyName("orig")]
        public string Original { get; set; }
    }
}
