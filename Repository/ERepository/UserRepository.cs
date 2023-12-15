

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using WebLibary.Models.Entities;

namespace WebLibary.Repository.ERepository
{
    public class UserRepository : IUserRepository
     {
        private readonly LibaryContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        public async Task<int> CreateUserAsync(User user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<bool> ValidatePasswordAsync(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
