using BookReview.Application.Models;
using BookReview.Core.Repositories;
using BookReview.Core.Services;
using MediatR;

namespace BookReview.Application.Commads.BookCommands.UpdateBookCover
{
    public class UpdateBookCoverHandler : IRequestHandler<UpdateBookCoverCommand, ResultViewModel>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICloudStorageService _cloudStorageService;

        public UpdateBookCoverHandler(IBookRepository bookRepository, ICloudStorageService cloudStorageService)
        {
            _bookRepository = bookRepository;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<ResultViewModel> Handle(UpdateBookCoverCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);

            if (book == null) 
                return ResultViewModel.Error("Livro não encontrado");

            byte[] imageBytes = Convert.FromBase64String(request.Base64BookCover);

            var fileName = book.Title.Trim() + "-" + Guid.NewGuid().ToString();

            using var memoryStream = new MemoryStream(imageBytes);

            var url = await _cloudStorageService.UploadBookCoverAsync(fileName, memoryStream);

            book.UpdateBookCover(url);

            await _bookRepository.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
