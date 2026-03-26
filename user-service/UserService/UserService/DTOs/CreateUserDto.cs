namespace UserService.DTOs
{
    public class CreateUserDto
    {
        public string Nom { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MotDePasse { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
