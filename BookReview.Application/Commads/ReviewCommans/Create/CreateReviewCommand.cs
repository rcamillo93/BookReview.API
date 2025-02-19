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

        public string Description { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public DateTime ReadingStartDate { get; set; }
    }
}
