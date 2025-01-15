using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.Create
{
    public class CreateUserCommand : IRequest<ResultViewModel<int>>
    {
        public CreateUserCommand(string fullName, string email, string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;        
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
