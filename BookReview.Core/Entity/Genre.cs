namespace BookReview.Core.Entity
{
    public class Genre : BaseEntity
    {
        public string Description { get; private set; }
        public List<Book> Books { get; private set; }

        public Genre(string description)
        {
            Description = description;     
            Books = new List<Book>();
        }

        public void Update(string description)
        {            
            Description = description;
        }
    }
}
