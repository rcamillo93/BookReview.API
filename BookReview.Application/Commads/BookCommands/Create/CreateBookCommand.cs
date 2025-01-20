using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Commads.BookCommands.Create
{
    public class CreateBookCommand : IRequest<ResultViewModel<int>>
    {
        public CreateBookCommand(string title, string description, string iSBN, int authorId, string publisher, int genreId, int publicationYear, int quantityPages, string bookCover)
        {
            Title = title;
            Description = description;
            ISBN = iSBN;
            AuthorId = authorId;
            Publisher = publisher;
            GenreId = genreId;
            PublicationYear = publicationYear;
            QuantityPages = quantityPages;
            BookCover = bookCover;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ISBN { get; private set; }
        public int AuthorId { get; private set; }
        public string Publisher { get; private set; }
        public int GenreId { get; private set; }
        public int PublicationYear { get; private set; }
        public int QuantityPages { get; private set; }
        public string BookCover { get; private set; }
    }
}
