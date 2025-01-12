using BookReview.Core.Entity;
using BookReview.Core.Repositories;

namespace BookReview.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        public Task AddAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
