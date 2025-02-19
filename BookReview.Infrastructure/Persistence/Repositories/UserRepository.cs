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

            query = query.Where(u => u.Active);

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

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password && u.Active);

            return user;
        }

        public async Task<bool> ValidateEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u =>u.Email == email && u.Active);
        }
    }

}
