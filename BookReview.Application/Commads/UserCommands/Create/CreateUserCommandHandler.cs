using BookReview.Application.Models;
using BookReview.Application.Services.Interfaces;
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
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService, IUserService userService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _userService = userService;
        }

        public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validateEmail = await _userService.ValidateEmail(request.Email);

            if (!validateEmail)
                return ResultViewModel<int>.Error("E-mail já cadastrado.");

            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = new User(request.FullName, request.Email, passwordHash);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();   

            return ResultViewModel<int>.Sucess(user.Id);
        }
    }
}
