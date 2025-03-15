namespace BookReview.Core.Services
{
    public interface IReportService
    {
        Task<byte[]> GenerateRatedBooksReport();
    }
}
