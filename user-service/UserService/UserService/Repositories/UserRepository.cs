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
        //  GET ALL 
        // SELECT * FROM utilisateurs
        public async Task<List<Utilisateur>> GetAll()
        {
            return await _context.Utilisateurs
                .ToListAsync();
        }

        //  GET BY ID 
        // SELECT * FROM utilisateurs WHERE id = {id}
        public async Task<Utilisateur?> GetById(int id)
        {
            return await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        //  GET BY ROLE 
        // SELECT * FROM utilisateurs WHERE role = {role}
        // utilisé pour chauffeurs et contrôleurs
        public async Task<List<Utilisateur>> GetByRole(
            string role)
        {
            return await _context.Utilisateurs
                .Where(u => u.Role == role)
                .ToListAsync();
        }

        //  EMAIL EXISTE 
        // vérifie si email déjà utilisé
        public async Task<bool> EmailExiste(string email)
        {
            return await _context.Utilisateurs
                .AnyAsync(u => u.Email == email);
        }

        //  CREATE 
        // INSERT INTO utilisateurs VALUES (...)
        public async Task<Utilisateur> Create(
            Utilisateur user)
        {
            _context.Utilisateurs.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //  UPDATE 
        // UPDATE utilisateurs SET ... WHERE id = {id}
        public async Task<Utilisateur> Update(
            Utilisateur user)
        {
            _context.Utilisateurs.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //  DELETE 
        // DELETE FROM utilisateurs WHERE id = {id}
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
        }
}
