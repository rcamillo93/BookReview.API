using BookReview.Core.Entity;

namespace BookReview.Core.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(int? authorId, string? title);
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task SaveChangesAsync();

        Task AddReview(Review review);
        Task<Review> GetReviewByIdAsync(int id);
    }
}
