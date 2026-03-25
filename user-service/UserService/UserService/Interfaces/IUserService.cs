using UserService.DTOs;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAll();

        // récupérer un utilisateur par id
        Task<UserResponseDto?> GetById(int id);

        // récupérer les chauffeurs
        Task<List<UserResponseDto>> GetChauffeurs();

        // récupérer les contrôleurs
        Task<List<UserResponseDto>> GetControleurs();

        // créer un utilisateur
        Task<UserResponseDto> Create(CreateUserDto dto);

        // modifier un utilisateur
        Task<UserResponseDto> Update(
            int id, UpdateUserDto dto);

        // supprimer un utilisateur
        Task<bool> Delete(int id);
    }
}
