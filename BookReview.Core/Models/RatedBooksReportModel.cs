namespace BookReview.Core.Models
{
    public class RatedBooksReportModel
    {
        public RatedBooksReportModel(int bookId, string author, string title, string genre, string bookCover, int qtdReviews, decimal? averageGrade)
        {
            BookId = bookId;
            Author = author;
            Title = title;      
            Genre = genre;
            BookCover = bookCover;
            QtdReviews = qtdReviews;
            AverageGrade = averageGrade;
        }

        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; } 
        public string Genre { get; set; }
        public string BookCover { get; set; }
        public string BookCoverBase64 { get; set; }
        public int QtdReviews { get; set; }
        public decimal? AverageGrade { get; set; }

        public RatedBooksReportModel()
        {
        }      
    }
}
