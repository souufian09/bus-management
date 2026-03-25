using UserService.Models;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Utilisateur>> GetAll();
        Task<Utilisateur?> GetById(int id);
        Task<List<Utilisateur>> GetByRole(string role);
        Task<bool> EmailExiste(string email);
        Task<Utilisateur> Create(Utilisateur user);
        Task<Utilisateur> Update(Utilisateur user);
        Task<bool> Delete(int id);


    }
}
