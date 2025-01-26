using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookReviewDbContext _dbContext;

        public UserRepository(BookReviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.AddAsync(user);
        }

        public async Task<List<User>> GetAllAsync(string? fullName)
        {
            var query = _dbContext.Users.AsQueryable();

            if(!string.IsNullOrEmpty(fullName))
                query = query.Where(u => EF.Functions.Like(u.FullName.ToUpper(), $"%{fullName.ToUpper()}%"));

            return await query.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }

}
