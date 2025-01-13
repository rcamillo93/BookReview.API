namespace BookReview.Core.Entity
{
    public class Author : BaseEntity
    {
        public Author(string fullName, DateTime dateBirth)
        {
            FullName = fullName;
            DateBirth = dateBirth;
            Books = new List<Book>();            
        }

        public string FullName { get; private set; }
        public DateTime DateBirth { get; private set; }

        public List<Book> Books { get; private set; }


        public void Update(string fullName, DateTime dateBirth)
        {
            FullName = fullName;
            DateBirth = dateBirth;
            UpdateAt = DateTime.Now;
        }
    }
}
