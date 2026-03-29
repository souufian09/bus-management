using System.Data;
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
        private UserResponseDto ToDto(Utilisateur u)
        {
            return new UserResponseDto
            {
                IdUtilisateur = u.Id,
                Nom = u.Nom,
                Email = u.Email,
                Role = u.Role
            };
        }
        public async Task<List<UserResponseDto>> GetAll()
        {
            var users = await _userRepo.GetAll();

            return users.Select(u => ToDto(u)).ToList();
        }

        public async Task<UserResponseDto?> GetById(int id)
        {
            var user = await _userRepo.GetById(id);

            if (user == null) return null;

            return ToDto(user);
        }

        public async Task<List<UserResponseDto>> GetChauffeurs()
        {
            var users = await _userRepo
                .GetByRole("CHAUFFEUR");

            return users.Select(u => ToDto(u)).ToList();
        }

        public async Task<List<UserResponseDto>> GetControleurs()
        {
            var users = await _userRepo
                .GetByRole("CONTROLEUR");

            return users.Select(u => ToDto(u)).ToList();
        }
        public async Task<List<UserResponseDto>>
            GetPassagers()
        {
            var users = await _userRepo
                .GetByRole("PASSAGER");
            return users.Select(u => ToDto(u)).ToList();
        }
        //new -------------------------------------------------------------------------------
       

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

            return ToDto(userCree);
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

            return ToDto(userModifie);
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _userRepo.GetById(id);
            if (user == null)
                throw new Exception(
                    "Utilisateur non trouvé");

            return await _userRepo.Delete(id);
        }

        //new-------------------------------------------------------------------------------
        public async Task<UserResponseDto> UpdateProfil(
        int id, UpdateProfilDto dto)
        {
            // vérifier que l'utilisateur existe
            var user = await _userRepo.GetById(id);
            if (user == null)
                throw new Exception(
                    "Utilisateur non trouvé");

            // modifier nom et email
            user.Nom = dto.Nom;
            user.Email = dto.Email;

            // modifier mot de passe seulement
            // si fourni dans le DTO
            // string.IsNullOrEmpty = vide ou null
            if (!string.IsNullOrEmpty(dto.MotDePasse))
                user.MotDePasse = dto.MotDePasse;

            var userModifie = await _userRepo.Update(user);
            return ToDto(userModifie);
            
            
        }

        // ── RECHERCHE ──
        public async Task<List<UserResponseDto>> Search(
            string? nom, string? email)
        {
            var users = await _userRepo.Search(nom, email);
            return users.Select(u => ToDto(u)).ToList();
        }
    }
}
