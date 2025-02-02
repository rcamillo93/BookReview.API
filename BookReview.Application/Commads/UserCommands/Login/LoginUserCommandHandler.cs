using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using BookReview.Core.Repositories;
using BookReview.Core.Services;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<ResultViewModel<LoginViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var hash = _authService.ComputeSha256Hash(request.Password);

            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, hash);

            if (user == null)
                return ResultViewModel<LoginViewModel>.Error("Usuário ou senha inválidos.");

            var token = _authService.GenerateJwtToken(user.Email, user.Role.ToString());

            var viewModel = new LoginViewModel(token);

            return ResultViewModel<LoginViewModel>.Sucess(viewModel);
        }
    }
}
