using BookReview.Application.Models;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Commads.BookCommands.Update
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ResultViewModel>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ResultViewModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);

            if (book is null)
                return ResultViewModel.Error("Livro não encontrado");

            book.Update(request.Title, request.Description, request.ISBN, request.AuthorId, request.Publisher, 
                        request.GenreId, request.PublicationYear, request.QuantityPages);

            await _bookRepository.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
