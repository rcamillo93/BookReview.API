using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.Create
{
    public class CreateUserCommand : IRequest<ResultViewModel<int>>
    {
        public CreateUserCommand(string fullName, string email, string password, string role)
        {
            FullName = fullName;
            Email = email;
            Password = password;      
            Role = role;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
    }
}
