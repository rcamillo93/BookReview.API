using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.ReviewCommans.Update
{
    public class UpdateReviewCommand : IRequest<ResultViewModel>
    {
        public UpdateReviewCommand(int id, string description, int rating)
        {
            Id = id;
            Description = description;
            Rating = rating;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public int Rating { get; private set; }
    }
}
