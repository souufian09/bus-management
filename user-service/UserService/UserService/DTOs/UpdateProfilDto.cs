namespace UserService.DTOs
{
    public class UpdateProfilDto
    {
        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? MotDePasse { get; set; }
    }
}
