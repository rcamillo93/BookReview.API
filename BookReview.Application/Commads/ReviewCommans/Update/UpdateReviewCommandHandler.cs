using BookReview.Application.Models;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Commads.ReviewCommans.Update
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ResultViewModel>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateReviewCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ResultViewModel> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _bookRepository.GetReviewByIdAsync(request.Id);

            if (review == null)
                return ResultViewModel.Error("Review não encontrado.");

            review.Update(request.Description, request.Rating);

            await _bookRepository.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
