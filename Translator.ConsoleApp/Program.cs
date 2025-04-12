using Translator.Core.Models.Translate;
using Translator.Core.Parse.Docx;
using Translator.Core.Parse.Tokens;
using Translator.Core.Translate.Api.Google;
using Translator.Core.Write.Docx;

namespace Translator.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TestApi();
        }

        static void TestApi()
        {
            var api = new GoogleTranslateApi()
            {
                TargetLanguage = Core.Translate.Languages.Language.English,
                Progress = new Progress<int>(x => Console.Write('+'))
            };
            var parser = new DocxParser() { Source = @"G:\test gala.docx", Mode = Core.Parse.ParseMode.Sentences };
            var tokens = parser.Parse();

            var writer = new DocxWriter() { Path = @"G:\test-gala-ed.docx", AddOriginal = true };

            foreach (var token in tokens)
            {
                IResponse? response = null;
                
                if (token.Type == TokenType.Sentence)
                {
                    var result = api.Translate(token.Content);

                    if (!result.Success)
                        throw new Exception(result.Message);

                    response = result.Body;
                }
                else
                {
                    response = 
                }


            }

            //var r = api.Translate(texts);

            //if (r.Any(x => x.Success != true))
            //    throw new Exception();

            writer.Write(r.Select(x => x.Body));
            //writer.Write(Mock.Responses);
        }

        static void TestMock()
        {
            //var api = new GoogleTranslateApi() { TargetLanguage = Core.Translate.Languages.Language.English };
            var parser = new DocxParser() { Source = @"G:\test.docx", Mode = Core.Parse.ParseMode.Sentences };
            var texts = parser.Parse();

            var writer = new DocxWriter() { Path = @"G:\test-ed.docx", AddOriginal = true };
            //var r = api.Translate(texts);

            //if (r.Any(x => x.Success != true))
            //    throw new Exception();

            //writer.Write(r.Select(x => x.Body));
            writer.Write(Mock.Responses);
        }
    }
}
