using BookReview.Application.Models;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using BookReview.Core.Services;
using MediatR;

namespace BookReview.Application.Commads.UserCommands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResultViewModel<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = new User(request.FullName, request.Email, passwordHash, request.Role);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();   

            return ResultViewModel<int>.Sucess(user.Id);
        }
    }
}
