using BookReview.Core.Entity;

namespace BookReview.Application.ViewModel
{
    public class BookViewModel
    {
        public BookViewModel(int id, string title, string description, string iSBN, int authorId, string publisher, 
                            int genreId, int publicationYear, int quantityPages, decimal? averageGrade, string bookCover)
        {
            Id = id;
            Title = title;
            Description = description;
            ISBN = iSBN;
            AuthorId = authorId;
            Publisher = publisher;
            GenreId = genreId;
            PublicationYear = publicationYear;
            QuantityPages = quantityPages;
            AverageGrade = averageGrade;
            BookCover = bookCover;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ISBN { get; private set; }
        public int AuthorId { get; private set; }
        public string Publisher { get; private set; }
        public int GenreId { get; private set; }
        public int PublicationYear { get; private set; }
        public int QuantityPages { get; private set; }
        public decimal? AverageGrade { get; private set; }
        public string BookCover { get; private set; }
    }
}
