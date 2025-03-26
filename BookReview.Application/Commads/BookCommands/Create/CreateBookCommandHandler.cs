using BookReview.Application.Models;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using BookReview.Core.Services;
using MediatR;
using System.IO;

namespace BookReview.Application.Commads.BookCommands.Create
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, ResultViewModel<int>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICloudStorageService _cloudStorageService;

        public CreateBookCommandHandler(IBookRepository bookRepository, ICloudStorageService cloudStorageService)
        {
            _bookRepository = bookRepository;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<ResultViewModel<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var validIsbn = await _bookRepository.GetBookByIsbn(request.ISBN);

            if (validIsbn != null)
                return ResultViewModel<int>.Error($"Já existe um livro com a ISBN {request.ISBN}");

            // criar um guid para imagem -- criar o path da imagem 
            var fileName = request.Title + "-" + Guid.NewGuid().ToString();

            byte[] imageBytes = Convert.FromBase64String(request.BookCover);

            using var memoryStream = new MemoryStream(imageBytes);

            // mandar a imagem para o cloud
            var url = await _cloudStorageService.UploadBookCoverAsync(fileName, memoryStream);

            var book = new Book(request.Title, request.Description, request.ISBN,
                                                      request.AuthorId, request.Publisher, request.GenreId,
                                                      request.PublicationYear, request.QuantityPages, url);
           
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            return ResultViewModel<int>.Sucess(book.Id);
        }
    }
}
