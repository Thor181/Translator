using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Core.Translate;

namespace Translator.ConsoleApp
{
    public static class Mock
    {
        public static IResponse[] Responses = [new MockResponse(), new MockResponse(), new MockResponse(),];
    }

    public class MockResponse : IResponse
    {
        public string Translation => "разбить на предложения";

        public string Original => "break into sentences";
    }
}
