using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Common
{
    internal static class SingleHttpClient
    {
        public static System.Net.Http.HttpClient Instance { get; private set; }

        static SingleHttpClient()
        {
            Instance = new System.Net.Http.HttpClient();
        }

        public static HttpResponseMessage Get(string url)
        {
            return Instance.GetAsync(url).GetAwaiter().GetResult();
        }
    }

    internal static class SingleHttpClientExtensions
    {
        public static string ReadAsString(this HttpContent httpContent)
        {
            return httpContent.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public static T? ReadFromJson<T>(this HttpContent httpContent)
        {
            return httpContent.ReadFromJsonAsync<T>().GetAwaiter().GetResult();
        }
    }
}
