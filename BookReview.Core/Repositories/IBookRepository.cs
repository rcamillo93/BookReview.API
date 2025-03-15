using BookReview.Core.Entity;
using BookReview.Core.Models;

namespace BookReview.Core.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(int? authorId, string? title);
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetBookByIsbn(string isbn);
        Task AddAsync(Book book);
        Task SaveChangesAsync();

        Task AddReview(Review review);
        Task<Review?> GetReviewByIdAsync(int id);
        Task<List<Review>> GetAllReviewsAsync(string? bookTitle);

        Task<int> CountReviewsByBookId(int bookId);

        Task<List<RatedBooksReportModel>> GetRatedBooksReport();
    }
}
