using BookReview.Core.Entity;

namespace BookReview.Core.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task SaveChangesAsync();

        Task AddReview(Review review);
    }
}
