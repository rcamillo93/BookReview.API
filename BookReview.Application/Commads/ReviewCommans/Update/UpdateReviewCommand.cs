using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.ReviewCommans.Update
{
    public class UpdateReviewCommand : IRequest<ResultViewModel>
    {
        public UpdateReviewCommand(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
    }
}
