using BookReview.Core.Entity;
using BookReview.Core.Repositories;

namespace BookReview.Infrastructure.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        public Task AddAsync(Genre genre)
        {
            throw new NotImplementedException();
        }

        public Task<List<Genre>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Genre?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
