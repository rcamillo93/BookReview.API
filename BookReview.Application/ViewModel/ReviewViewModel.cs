namespace BookReview.Application.ViewModel
{
    public class ReviewViewModel
    {
        public ReviewViewModel(string description, string bookTitle, string bookCover, int bookId, int rating)
        {
            Description = description;
            BookTitle = bookTitle;
            BookCover = bookCover;
            BookId = bookId;
            Rating = rating;
        }

        public string Description { get; set; }
        public string BookTitle { get; set; }
        public string BookCover { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
    }
}
