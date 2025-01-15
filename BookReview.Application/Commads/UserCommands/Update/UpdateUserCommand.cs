using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.Update
{
    public class UpdateUserCommand : IRequest<ResultViewModel>
    {
        public UpdateUserCommand(int id, string? fullName, string? email, bool? active)
        {
            Id = id;
            FullName = fullName;
            Email = email;    
            Active = active;
        }

        public int Id { get; private set; }
        public string? FullName { get; private set; }
        public string? Email { get; private set; }
        public bool? Active { get; private set; }
    }
}
