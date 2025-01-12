using BookReview.Core.Entity;

namespace BookReview.Core.Repositories
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllAsync();
        Task<Genre?> GetByIdAsync(int id);
        Task AddAsync(Genre book);
        Task SaveChangesAsync();
    }
}
