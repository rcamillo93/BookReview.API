﻿using BookReview.Core.Entity;

namespace BookReview.Core.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(string? fullName);
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User book);
        Task SaveChangesAsync();
    }
}
