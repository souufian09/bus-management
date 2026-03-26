using UserService.DTOs;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<List<UserResponseDto>> GetAll()
        {
            var users = await _userRepo.GetAll();

            return users.Select(u => new UserResponseDto
            {
                IdUtilisateur = u.Id,
                Nom = u.Nom,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }

        public async Task<UserResponseDto?> GetById(int id)
        {
            var user = await _userRepo.GetById(id);

            if (user == null) return null;

            return new UserResponseDto
            {
                IdUtilisateur = user.Id,
                Nom = user.Nom,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<List<UserResponseDto>> GetChauffeurs()
        {
            var chauffeurs = await _userRepo
                .GetByRole("CHAUFFEUR");

            return chauffeurs.Select(u => new UserResponseDto
            {
                IdUtilisateur = u.Id,
                Nom = u.Nom,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }

        public async Task<List<UserResponseDto>> GetControleurs()
        {
            var controleurs = await _userRepo
                .GetByRole("CONTROLEUR");

            return controleurs.Select(u => new UserResponseDto
            {
                IdUtilisateur = u.Id,
                Nom = u.Nom,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }

        public async Task<UserResponseDto> Create(
            CreateUserDto dto)
        {

            if (await _userRepo.EmailExiste(dto.Email))
                throw new Exception("Email déjà utilisé");

            var user = new Utilisateur
            {
                Nom = dto.Nom,
                Email = dto.Email,
                MotDePasse = dto.MotDePasse,
                Role = dto.Role
            };

            var userCree = await _userRepo.Create(user);

            return new UserResponseDto
            {
                IdUtilisateur = userCree.Id,
                Nom = userCree.Nom,
                Email = userCree.Email,
                Role = userCree.Role
            };
        }

        public async Task<UserResponseDto> Update(
            int id, UpdateUserDto dto)
        {
            var user = await _userRepo.GetById(id);
            if (user == null)
                throw new Exception(
                    "Utilisateur non trouvé");

            user.Nom = dto.Nom;
            user.Email = dto.Email;

            var userModifie = await _userRepo.Update(user);

            return new UserResponseDto
            {
                IdUtilisateur = userModifie.Id,
                Nom = userModifie.Nom,
                Email = userModifie.Email,
                Role = userModifie.Role
            };
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _userRepo.GetById(id);
            if (user == null)
                throw new Exception(
                    "Utilisateur non trouvé");

            return await _userRepo.Delete(id);
        }
    }
}
