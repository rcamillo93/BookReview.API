using BookReview.Core.Entity;
using BookReview.Core.Repositories;

namespace BookReview.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public Task AddAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Review?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
