using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.ReviewCommans.Create
{
    public class CreateReviewCommand : IRequest<ResultViewModel<int>>
    {
        public CreateReviewCommand(string description, int userId, int bookId, int rating, DateTime readingStartDate)
        {
            Description = description;
            UserId = userId;
            BookId = bookId;
            Rating = rating;
            ReadingStartDate = readingStartDate;
        }

        public string Description { get; private set; }
        public int UserId { get; private set; }
        public int BookId { get; private set; }
        public int Rating { get; private set; }
        public DateTime ReadingStartDate { get; private set; }
    }
}
