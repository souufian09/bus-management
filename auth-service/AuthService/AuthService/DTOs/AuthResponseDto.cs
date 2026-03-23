namespace AuthService.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int IdUtilisateur { get; set; }
        public string Nom { get; set; } = string.Empty;
    }
}
