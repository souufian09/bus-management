using AuthService.Data;
using AuthService.Models;
using AuthService.Services;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Utilisateur?> FindByEmail(string email)
        {

            return await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> EmailExiste(string email)
        {

            return await _context.Utilisateurs
                .AnyAsync(u => u.Email == email);
        }
        public async Task<Utilisateur> Sauvegarder(
            Utilisateur user)
        {
            _context.Utilisateurs.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

    }
}
