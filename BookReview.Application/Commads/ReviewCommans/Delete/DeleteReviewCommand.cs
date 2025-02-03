using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.ReviewCommans.Delete
{
    public class DeleteReviewCommand : IRequest<ResultViewModel>
    {
        public DeleteReviewCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
