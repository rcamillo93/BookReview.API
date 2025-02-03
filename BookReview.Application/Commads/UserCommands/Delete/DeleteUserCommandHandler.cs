using BookReview.Application.Models;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultViewModel>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if(user == null)
                return ResultViewModel.Error("Usuário não encontrado");

            user.UpdateStatus(false);

            await _userRepository.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
