using AuthService.Models;

namespace AuthService.Services
{
    public interface IUserRepository
    {
        Task<Utilisateur?> FindByEmail(string email);
        Task<bool> EmailExiste(string email);
        Task<Utilisateur> Sauvegarder(Utilisateur user);
    }
}
