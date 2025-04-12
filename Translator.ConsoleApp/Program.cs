using Translator.Core.Parse.Word;
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
            var progress = new Progress<int>(x => Console.Write('+'));
            var api = new GoogleTranslateApi() { TargetLanguage = Core.Translate.Languages.Language.English, Progress = progress };
            var parser = new DocxParser() { Source = @"G:\test gala.docx", Mode = Core.Parse.ParseMode.Sentences };
            var texts = parser.Parse();

            var writer = new DocxWriter() { Path = @"G:\test-gala-ed.docx", AddOriginal = true };
            var r = api.Translate(texts);

            if (r.Any(x => x.Success != true))
                throw new Exception();

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
