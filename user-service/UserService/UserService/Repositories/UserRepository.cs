using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Utilisateur>> GetAll()
        {
            return await _context.Utilisateurs
                .ToListAsync();
        }

        public async Task<Utilisateur?> GetById(int id)
        {
            return await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<List<Utilisateur>> GetByRole(
            string role)
        {
            return await _context.Utilisateurs
                .Where(u => u.Role == role)
                .ToListAsync();
        }

        public async Task<bool> EmailExiste(string email)
        {
            return await _context.Utilisateurs
                .AnyAsync(u => u.Email == email);
        }

        public async Task<Utilisateur> Create(
            Utilisateur user)
        {
            _context.Utilisateurs.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Utilisateur> Update(
            Utilisateur user)
        {
            _context.Utilisateurs.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(int id)
        {
            // chercher l'utilisateur
            var user = await GetById(id);

            // si pas trouvé → retourner false
            if (user == null) return false;

            // supprimer et sauvegarder
            _context.Utilisateurs.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Utilisateur>> Search(
            string? nom, string? email)
        {
            var query = _context.Utilisateurs
                .AsQueryable();

            if (!string.IsNullOrEmpty(nom))
                query = query.Where(
                    u => u.Nom.Contains(nom));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(
                    u => u.Email.Contains(email));

            return await query.ToListAsync();
        }
    }
}
