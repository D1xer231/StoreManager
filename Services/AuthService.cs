using Microsoft.EntityFrameworkCore;
using StoreManager.Models;

namespace StoreManager.Services
{
    public class AuthService
    {
        public async Task<User?> LoginAsync(string username, string password)
        {
            using var db = new AppDbContext();

            var user = await db.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !user.IsActive)
                return null;

            bool isPasswordValid = PasswordService.VerifyPassword(
                password,
                user.PasswordHash
            );

            if (!isPasswordValid)
                return null;

            user.LastLogin = DateTime.Now;

            await db.SaveChangesAsync();

            return user;
        }
    }
}