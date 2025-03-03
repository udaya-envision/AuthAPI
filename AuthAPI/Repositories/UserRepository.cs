using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AuthAPI.Data;
using AuthAPI.Models;

namespace AuthAPI.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> RegisterUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
