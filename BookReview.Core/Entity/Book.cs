namespace BookReview.Core.Entity
{
    public class Book : BaseEntity
    {
        public Book(string title, string description, string iSBN, int authorId, 
                    string publisher, int genreId, int publicationYear, 
                    int quantityPages, string bookCover)
        {
            Title = title;
            Description = description;
            ISBN = iSBN;
            AuthorId = authorId;
            Publisher = publisher;
            GenreId = genreId;
            PublicationYear = publicationYear;
            QuantityPages = quantityPages;
            AverageGrade = null;
            BookCover = bookCover;
            UpdateAt = null;

            Reviews = new List<Review>();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ISBN { get; private set; }
        public int AuthorId { get; private set; }
        public string Publisher { get; private set; }
        public int GenreId { get; private set; }
        public int PublicationYear{ get; private set; }
        public int QuantityPages { get; private set; }
        public decimal? AverageGrade { get; private set; }
        public string BookCover { get; private set; }

        public Author Author { get; private set; }
        public List<Review> Reviews { get; private set; }

    }
}
