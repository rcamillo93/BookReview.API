using BookReview.Core.Entity;
using BookReview.Core.Models;
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
            return await _dbContext.Books
                            .Include(b => b.Reviews)                            
                            .SingleOrDefaultAsync(b => b.Id == id);
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

            query = query.Where(r => r.Deleted == false);

            return await query.ToListAsync();
        }

        public async Task<int> CountReviewsByBookId(int bookId)
        {
            return await _dbContext.Reviews.CountAsync(r => r.BookId == bookId && r.Deleted == false);
        }

        public async Task<Book?> GetBookByIsbn(string isbn)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<List<RatedBooksReportModel>> GetRatedBooksReport()
        {
            var query = from b in _dbContext.Books
                        join a in _dbContext.Authors on b.AuthorId equals a.Id
                        join r in _dbContext.Reviews on b.Id equals r.BookId
                        join g in _dbContext.Genres on b.GenreId equals g.Id
                        where r.Deleted == false                     
                        group new { b, a, g } by new
                        {
                            b.Id,
                            a.FullName,
                            b.Title,
                            g.Description,
                            b.BookCover,                            
                            b.AverageGrade
                        } into g
                        select new RatedBooksReportModel
                        {
                            BookId = g.Key.Id,
                            Author = g.Key.FullName,
                            Title = g.Key.Title,                          
                            Genre = g.Key.Description,
                            BookCover = g.Key.BookCover,
                            QtdReviews = g.Count(),
                            AverageGrade = g.Key.AverageGrade
                        };

            return await query.OrderByDescending(x => x.AverageGrade).ToListAsync();
        }
    }
}
