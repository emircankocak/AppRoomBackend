using System.ComponentModel.DataAnnotations;
namespace AppRoomGameBackend.DTOs.User

{
    public class RegisterDto
    {
        [Required(ErrorMessage="Kullanıcı adı boş bırakılamaz.")]
        public string Username { get; set; } = string.Empty;
        
        [Required(ErrorMessage="Email boş bırakılamaz.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage="Şifre boş bırakılamaz.")]
        public string Password { get; set; } = string.Empty;
        
        public string Role { get; set; } = string.Empty;
    }
}
