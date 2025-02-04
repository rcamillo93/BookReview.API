namespace BookReview.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ValidateEmail(string email);
    }
}
