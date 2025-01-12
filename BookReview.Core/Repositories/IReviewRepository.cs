using BookReview.Core.Entity;

namespace BookReview.Core.Repositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task AddAsync(Review book);
        Task SaveChangesAsync();
    }
}
