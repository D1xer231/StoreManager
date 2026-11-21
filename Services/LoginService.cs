using System;
using System.Linq;
using System.Windows.Controls;

namespace StoreManager.Services
{
    internal class LoginService
    {
        private readonly AppDbContext _context;

        public LoginService(
            TextBox username,
            PasswordBox password,
            AppDbContext context)
        {
            if (username == null || password == null)
                throw new ArgumentNullException();

            _context = context;
        }

        public bool Login(TextBox username, PasswordBox password)
        {
            // Find user in database
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username.Text);

            // Check if user exists
            if (user == null)
                return false;

            // Compare passwords
            if (user.PasswordHash != password.Password)
                return false;

            return true;
        }
    }
}
