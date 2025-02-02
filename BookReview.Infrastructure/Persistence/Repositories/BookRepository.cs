using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookReviewDbContext _dbContext;

        public BookRepository(BookReviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Book book)
        {
            await _dbContext.AddAsync(book);
        }

        public async Task<List<Book>> GetAllAsync(int? authorId, string? title)
        {
            var query = _dbContext.Books.AsQueryable();

            if (authorId.HasValue)
                query = query.Where(b => b.AuthorId == authorId.Value);

            if (!string.IsNullOrEmpty(title))
                query = query.Where(b => EF.Functions.Like(b.Title.ToUpper(), $"%{title.ToUpper()}%"));

            return await query.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


        public async Task AddReview(Review review)
        {
            await _dbContext.AddAsync(review);
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            return await _dbContext.Reviews
                .Include(b => b.Book)
                .Include(u => u.User)
                .Include(a => a.Book.Author)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Review>> GetAllReviewsAsync(string? bookTitle)
        {
            var query = _dbContext.Reviews
                        .Include(r => r.Book)
                        .AsQueryable();

            if (!string.IsNullOrEmpty(bookTitle))
                query = query.Where(r => EF.Functions.Like(r.Book.Title.ToUpper(), $"%{bookTitle.ToUpper()}%"));

            return await query.ToListAsync();
        }

        public async Task<int> CountReviewsByBookId(int bookId)
        {
            return await _dbContext.Reviews.CountAsync(r => r.BookId == bookId);
        }
    }
}
