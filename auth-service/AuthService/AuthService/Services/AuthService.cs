using AuthService.DTOs;
using AuthService.Models;

namespace AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;
        public AuthService(
            IUserRepository userRepo,
            IJwtService jwtService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
        }
        public async Task<AuthResponseDto> Login(
            LoginDto loginDto)
        {
            var user = await _userRepo
                .FindByEmail(loginDto.Email);
            if (user == null)
                throw new Exception("Email incorrect");

            if (user.MotDePasse != loginDto.MotDePasse)
                throw new Exception(
                    "Mot de passe incorrect");

            var token = _jwtService.GenererToken(user);
            return new AuthResponseDto
            {
                Token = token,
                Role = user.Role,
                IdUtilisateur = user.Id,
                Nom = user.Nom
            };
        }
        public async Task<object> Register(
            RegisterDto registerDto)
        {
            if (await _userRepo
                .EmailExiste(registerDto.Email))
                throw new Exception(
                    "Email déjà utilisé");

            var user = new Utilisateur
            {
                Nom = registerDto.Nom,
                Email = registerDto.Email,
                MotDePasse = registerDto.MotDePasse,
                Role = registerDto.Role
            };
            var userSauvegarde = await _userRepo
                .Sauvegarder(user);

            return new
            {
                idUtilisateur = userSauvegarde.Id,
                message = "Compte créé avec succès"
            };
        }
    }
}
