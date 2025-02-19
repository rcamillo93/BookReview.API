using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.ForgotPassword
{
    public class ForgotPassowrdCommand : IRequest<ResultViewModel<string>>
    {
        public ForgotPassowrdCommand(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
