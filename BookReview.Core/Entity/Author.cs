namespace BookReview.Core.Entity
{
    public class Author : BaseEntity
    {
        public Author(string fullName)
        {
            FullName = fullName;
            Books = new List<Book>();
        }

        public string FullName { get; private set; }

        public List<Book> Books { get; private set; }
    }
}
