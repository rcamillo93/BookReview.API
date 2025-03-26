using BookReview.Core.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BookReview.Infrastructure.Reports
{
    public class RatedBooksReport : IDocument
    {
        private readonly List<RatedBooksReportModel> _data;

        public RatedBooksReport(List<RatedBooksReportModel> data)
        {
            _data = data;
        }

        public void Compose(IDocumentContainer container)
        {
            var groupedGenres = _data
              .GroupBy(r => new { r.Genre })
              .OrderBy(r => r.Key.Genre);

            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .AlignCenter()
                    .Column(column =>
                    {
                        // Título
                        column.Item().Text(t =>
                        {
                            t.Span("Top Rated Books")
                                .SemiBold().FontSize(18);
                        });
                    });

                page.Content()
                  .PaddingVertical(1, Unit.Centimetre)
                  .Column(column =>
                  {

                      foreach (var group in groupedGenres)
                      {

                          column.Item().Text($"{group.Key.Genre}")
                           .FontSize(14)
                           .Bold()
                           .AlignLeft();
                          
                          column.Spacing(10);

                          column.Item().Table(t =>
                          {
                              t.ColumnsDefinition(c =>
                              {
                                  c.RelativeColumn(3);
                                  c.RelativeColumn(2);
                                  c.RelativeColumn(2);
                                  c.RelativeColumn(3);
                                  c.RelativeColumn(3);
                              });

                              t.Cell().Row(1).Column(1).Element(Block).Text("Title").SemiBold();
                              t.Cell().Row(1).Column(2).Element(Block).Text("Author").SemiBold();
                              t.Cell().Row(1).Column(3).Element(Block).Text("QtReviews").SemiBold();
                              t.Cell().Row(1).Column(4).Element(Block).Text("BookCover").SemiBold();
                              t.Cell().Row(1).Column(5).Element(Block).Text("AverageGrade").SemiBold();

                              uint rowIndex = 2;

                              foreach (var book in group)
                              {          
                                  t.Cell().Row(rowIndex).Column(1).Element(Entry).Text(book.Title);
                                  t.Cell().Row(rowIndex).Column(2).Element(Entry).Text(book.Author);
                                  t.Cell().Row(rowIndex).Column(3).Element(Entry).Text(book.QtdReviews.ToString());                                 

                                  if (!string.IsNullOrEmpty(book.BookCoverBase64))
                                  {
                                      t.Cell().Row(rowIndex).Column(4).Element(Entry).Image(Base64ToByteArray(book.BookCoverBase64));
                                  }                      
                                        

                                  t.Cell().Row(rowIndex).Column(5).Element(Entry).Text(book.AverageGrade.ToString());

                                  rowIndex++;
                              }
                          });                    
                      }
                  });

                page.Footer()
                    .AlignRight()
                    .Text(x =>
                    {
                        x.Span($"Generated: {DateTime.Now:dd/MM/yyyy} - Page ");
                        x.CurrentPageNumber();
                    });
            });
        }

        static IContainer Entry(IContainer container)
        {
            return container
                   .BorderBottom(1)
                   .PaddingVertical(1)
                   .PaddingHorizontal(6)
                   .ShowOnce()
                   .AlignCenter()
                   .AlignMiddle();
        }

        static IContainer Block(IContainer container)
        {
            return container
                   .BorderBottom(1)
                   .Background(Colors.Grey.Lighten3)
                   .ShowOnce()
                   .MinWidth(50)
                   .MinHeight(20)
                   .AlignCenter()
                   .AlignMiddle();
        }

        private static byte[] Base64ToByteArray(string base64)
        {
            return Convert.FromBase64String(base64);
        }
    }
}