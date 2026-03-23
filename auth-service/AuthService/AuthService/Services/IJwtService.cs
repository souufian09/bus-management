using AuthService.Models;

namespace AuthService.Services
{
    public interface IJwtService
    {
        string GenererToken(Utilisateur user);
    }
}
