using BookReview.Application.Models;
using BookReview.Core.Repositories;
using BookReview.Core.Services;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.RecoveryPassword
{
    public class RecoverPasswordHandler : IRequestHandler<RecoverPasswordCommand, ResultViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public RecoverPasswordHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<ResultViewModel> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
                return ResultViewModel.Error("Usuário não encontrado.");

            var temporaryHash = _authService.ComputeSha256Hash(request.TemporaryPassword);
            var newHash = _authService.ComputeSha256Hash(request.NewPassword);

            if ((user.TemporaryPassword == null || temporaryHash != user.TemporaryPassword) || DateTime.Now > user.ValidateHash)
            {
               return ResultViewModel.Error("Usuário ou senha inválidos.");
            }

            user.UpdatePassword(newHash);

            await _userRepository.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
