using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.RecoveryPassword
{
    public class RecoverPasswordCommand :IRequest<ResultViewModel>
    {
        public RecoverPasswordCommand(string email, string temporaryPassword, string newPassword)
        {
            Email = email;
            TemporaryPassword = temporaryPassword;
            NewPassword = newPassword;
        }

        public string Email { get; set; }
        public string TemporaryPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
