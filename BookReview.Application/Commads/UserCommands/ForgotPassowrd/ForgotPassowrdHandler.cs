using BookReview.Application.Models;
using BookReview.Core.Repositories;
using BookReview.Core.Services;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.ForgotPassword
{
    public class ForgotPassowrdHandler : IRequestHandler<ForgotPassowrdCommand, ResultViewModel<string>>
    {
        private readonly IUserRepository _userRepository;        
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public ForgotPassowrdHandler(IUserRepository userRepository, IAuthService authService, IEmailService emailService)
        {
            _userRepository = userRepository;     
            _authService = authService;
            _emailService = emailService;
        }

        public async Task<ResultViewModel<string>> Handle(ForgotPassowrdCommand request, CancellationToken cancellationToken)
        {     
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
                return ResultViewModel<string>.Error("Usuário não encontrado.");

            var randomPassword = _authService.GenerateTemporaryPassword(8);

            var temporaryPassword = _authService.ComputeSha256Hash(randomPassword);

            user.StartRecoveryPassword(temporaryPassword);

            await _userRepository.SaveChangesAsync();

            await _emailService.SendRecoveryPasswordEmailAsync(user, randomPassword, temporaryPassword);

            return ResultViewModel<string>.Sucess(temporaryPassword);
        }
    }
}
