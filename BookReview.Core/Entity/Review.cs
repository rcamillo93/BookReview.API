namespace BookReview.Core.Entity
{
    public class Review : BaseEntity
    {
        public Review(string description, int userId, int bookId, int rating)
        {
            Description = description;
            UserId = userId;
            BookId = bookId;
            Rating = rating;
        }

        public string Description { get; private set; }
        public int UserId { get; private set; }
        public int BookId { get; private set; }
        public int Rating { get; private set; }

        public Book Book { get; private set; }
        public User User { get; private set; }
    }
}
