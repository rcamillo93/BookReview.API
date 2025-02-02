namespace BookReview.Application.ViewModel
{
    public class ReviewDetailsViewModel
    {
        public ReviewDetailsViewModel(string description, int userId, int bookId, int rating, string userName,
                               string bookTitle, string bookCover, string authorName)
        {
            Description = description;
            UserId = userId;
            BookId = bookId;
            Rating = rating;
            UserName = userName;
            BookTitle = bookTitle;
            BookCover = bookCover;
            AuthorName = authorName;      
        }

        public string Description { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string UserName { get; set; }
        public string BookTitle { get; set; }
        public string BookCover { get; set; }
        public string AuthorName { get; set; }
        public DateTime ReadingStartDate { get; set; }
    }
}
