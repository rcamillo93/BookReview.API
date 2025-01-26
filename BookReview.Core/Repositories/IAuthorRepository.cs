using BookReview.Core.Entity;

namespace BookReview.Core.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync(string? fullName);
        Task<Author?> GetByIdAsync(int id);
        Task AddAsync(Author book);
        Task SaveChangesAsync();
    }
}
