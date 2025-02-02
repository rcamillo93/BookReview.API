namespace BookReview.Application.ViewModel
{
    public class LoginViewModel
    {
        public LoginViewModel(string token)
        {
            Token = token;
        }

        public string Token { get; private set; }
    }
}
