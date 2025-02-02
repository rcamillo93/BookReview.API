using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.Login
{
    public class LoginUserCommand : IRequest<ResultViewModel<LoginViewModel>>
    {
        public LoginUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; private set; }
        public string Password { get; set; }
    }
}
