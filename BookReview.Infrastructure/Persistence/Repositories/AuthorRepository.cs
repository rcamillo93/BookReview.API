using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Infrastructure.Persistence.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookReviewDbContext _dbContext;

        public AuthorRepository(BookReviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Author author)
        {
            await _dbContext.AddAsync(author);
        }

        public async Task<List<Author>> GetAllAsync(string? fullName)
        {
            var query = _dbContext.Authors.AsQueryable();

            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(a => EF.Functions.Like(a.FullName.ToUpper(), $"%{fullName.ToUpper()}%"));

            return await query.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _dbContext.Authors.SingleOrDefaultAsync(a  => a.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
