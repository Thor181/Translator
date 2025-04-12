using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Core.Models.Translate;
using Translator.Core.Parse.Tokens;

namespace Translator.Core.Write.Docx
{
    public class DocxWriter : IWriter
    {
        private int _commentId = 0;
        public required string Path { get; set; }
        public bool AddOriginal { get; set; } = true;

        public void Write(params IEnumerable<IResponse> texts)
        {
            using var document = CreateFile();

            foreach (var item in texts)
            {
                var paragraph = AddParagraph(document, item.Translation, item.Original);
            }

            document.Save();
        }

        public void Write(params IEnumerable<BaseToken> tokens)
        {
            using var document = CreateFile();

            foreach (var item in tokens)
            {
                if (item is TranslationToken translationToken)
                {
                    AddParagraph(document, translationToken.Content, translationToken.Original);
                }
                else
                {
                    //TODO: add processing for paragraph token
                }

            }
        }

        private WordprocessingDocument CreateFile()
        {
            File.Copy(System.IO.Path.Combine("templates", "template.docx"), Path, true);

            var file = WordprocessingDocument.Open(Path, true);
            var commentsPart = file.MainDocumentPart.AddNewPart<WordprocessingCommentsPart>();
            commentsPart.Comments = new Comments();

            _commentId = default;

            return file;
        }

        private Paragraph AddParagraph(WordprocessingDocument file, string translation, string? original)
        {
            var textElement = new Text(translation);
            var runElement = new Run(textElement);

            Paragraph paragraph = new Paragraph(runElement);

            if (AddOriginal && original != null)
            {
                var commentId = _commentId++.ToString();

                var commentParagraph = new Paragraph(new Run(new Text(original)));
                var comment = new Comment()
                {
                    Id = commentId,
                    Author = "Translator",
                    Initials = "T",
                    Date = DateTime.Now,
                };
                comment.AppendChild(commentParagraph);

                file.MainDocumentPart!.WordprocessingCommentsPart!.Comments.AppendChild(comment);

                paragraph.InsertBefore(new CommentRangeStart() { Id = commentId }, paragraph.GetFirstChild<Run>());
                var commentEnd = paragraph.InsertAfter(new CommentRangeEnd() { Id = commentId }, paragraph.Elements<Run>().First());

                paragraph.InsertAfter(new Run(new CommentReference() { Id = commentId }), commentEnd);

            }

            file.MainDocumentPart!.Document.Body!.AppendChild(paragraph);

            return paragraph;
        }
    }
}
