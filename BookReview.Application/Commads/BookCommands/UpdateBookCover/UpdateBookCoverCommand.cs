using BookReview.Application.Models;
using MediatR;
using System.Buffers.Text;

namespace BookReview.Application.Commads.BookCommands.UpdateBookCover
{
    public class UpdateBookCoverCommand : IRequest<ResultViewModel>
    {
        public int BookId { get; set; }
        public string Base64BookCover { get; set; }
    }
}
