using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Translator.Core.Models.Utils;
using Translator.Core.Parse.Tokens;

namespace Translator.Core.Parse.Docx
{
    public sealed class DocxParser : IParser, IDisposable
    {
        private bool _disposed;
        private WordprocessingDocument _document;
        private Body _documentBody;

        public ParseMode Mode { get; set; } = ParseMode.Paragraphs;
        public required string Source { get; set; }

        public ReadOnlySpan<BaseToken> Parse()
        {
            OpenFile();

            var tokens = Tokenize();

            return tokens;
        }

        private void OpenFile()
        {
            _document = WordprocessingDocument.Open(Source, false);
            _documentBody = _document.MainDocumentPart?.Document.Body!;

            if (_documentBody == null)
                throw new NullReferenceException("Document body must not be null");
        }

        private ReadOnlySpan<BaseToken> Tokenize()
        {
            var paragraphs = _documentBody.Elements<Paragraph>();
            var tokens = new List<BaseToken>();

            foreach (var paragraph in paragraphs)
            {
                var text = paragraph.Select(x => x.Descendants<Text>().Select(y => y.Text).JoinString()).JoinString();
                ReadOnlySpan<string> sentences = SplitIntoSentences(text);

                foreach (var sentence in sentences)
                    tokens.Add(new SentenceToken(sentence));

                tokens.Add(new ParagraphToken());
            }
            
            return CollectionsMarshal.AsSpan(tokens);
        }

        private ReadOnlySpan<string> GetTextParagraphs()
        {
            var paragraphs = _documentBody.Elements<Paragraph>();

            var texts = paragraphs.Select(x => string.Join(string.Empty, x.Descendants<Text>().Select(y => y.Text))).Where(x => !string.IsNullOrEmpty(x));
            ReadOnlySpan<string> textsStrings = texts.ToArray();

            if (Mode == ParseMode.Sentences)
            {
                var text = string.Join(string.Empty, textsStrings);
                textsStrings = SplitIntoSentences(text);
            }

            return textsStrings;
        }

        private static ReadOnlySpan<string> SplitIntoSentences(ReadOnlySpan<char> text)
        {
            var dotsCount = text.Count('.');

            var sentences = new List<string>(dotsCount);

            var index = 0;
            var previousIndex = 0;

            while (index < text.Length)
            {
                var current = text[index];

                if (current == '.')
                {
                    var sentence = text.Slice(previousIndex, index - previousIndex);
                    sentences.Add(sentence.ToString());

                    index++;//skip the dot

                    if (index >= text.Length)
                        continue;

                    while (!char.IsLetter(text[index]))
                        index++; //skip non letters

                    previousIndex = index;
                    continue;
                }

                index++;
            }

            return CollectionsMarshal.AsSpan(sentences);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _document?.Dispose();
                _disposed = true;
            }
        }
    }
}
