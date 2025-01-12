namespace BookReview.Application.ViewModel
{
    public class AuthorViewModel
    {
        public AuthorViewModel(string fullName, DateTime dateBirth)
        {
            FullName = fullName;
            DateBirth = dateBirth;
        }

        public string FullName { get; private set; }
        public DateTime DateBirth { get; private set; }
    }
}
