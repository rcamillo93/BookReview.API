namespace BookReview.Core.Entity
{
    public class Book : BaseEntity
    {
        public Book(string title, string description, string iSBN, string author, 
                    string publisher, int genreId, int publicationYear, 
                    int quantityPages, decimal average, string bookCover)
        {
            Title = title;
            Description = description;
            ISBN = iSBN;
            Author = author;
            Publisher = publisher;
            GenreId = genreId;
            PublicationYear = publicationYear;
            QuantityPages = quantityPages;
            Average = average;
            BookCover = bookCover;
            UpdateAt = CreatedAt;

            Reviews = new List<Review>();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ISBN { get; private set; }
        public string Author { get; private set; }
        public string Publisher { get; private set; }
        public int GenreId { get; private set; }
        public int PublicationYear{ get; private set; }
        public int QuantityPages { get; private set; }
        public decimal Average { get; private set; }
        public string BookCover { get; private set; }

        public List<Review> Reviews { get; private set; }

    }
}
