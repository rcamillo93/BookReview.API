using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.Delete
{
    public class DeleteUserCommand : IRequest<ResultViewModel>
    {
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
