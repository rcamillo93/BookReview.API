using BookReview.Core.Entity;

namespace BookReview.Core.Services
{
    public interface IEmailService
    {
       Task SendRecoveryPasswordEmailAsync(User user, string temporaryPassword, string temporaryHash);
    }
}
