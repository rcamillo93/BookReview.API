﻿namespace BookReview.Core.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, string role);
        string ComputeSha256Hash(string password);
        string GenerateTemporaryPassword(int length);
    }
}
