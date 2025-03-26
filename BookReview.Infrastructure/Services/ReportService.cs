using BookReview.Core.Repositories;
using BookReview.Core.Services;
using BookReview.Infrastructure.Reports;
using QuestPDF.Fluent;

namespace BookReview.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICloudStorageService _cloudStorageService;

        public ReportService(IBookRepository bookRepository, ICloudStorageService cloudStorageService)
        {
            _bookRepository = bookRepository;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<byte[]> GenerateRatedBooksReport()
        {
            var data = await _bookRepository.GetRatedBooksReport();

            foreach (var book in data)
            {
                if (!string.IsNullOrEmpty(book.BookCover))
                {
                    book.BookCoverBase64 = await _cloudStorageService.GetBookCoverAsync(book.BookCover);
                }
            }
            
            var document = new RatedBooksReport(data);
            return document.GeneratePdf();
        }
    }
}
