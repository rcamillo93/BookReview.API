using BookReview.Core.Repositories;
using BookReview.Core.Services;
using BookReview.Infrastructure.Reports;
using QuestPDF.Fluent;

namespace BookReview.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly IBookRepository _bookRepository;

        public ReportService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<byte[]> GenerateRatedBooksReport()
        {
            var data = await _bookRepository.GetRatedBooksReport();

            var document = new RatedBooksReport(data);
            return document.GeneratePdf();
        }
    }
}
