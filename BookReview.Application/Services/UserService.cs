using BookReview.Application.Models;
using BookReview.Application.Services.Interfaces;
using BookReview.Core.Repositories;
using MediatR;

namespace BookReview.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ValidateEmail(string email)
        {
            var user = await _userRepository.ValidateEmail(email);

            if (user)
                return false;

            return true;
        }
    }
}
