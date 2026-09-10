using System.ComponentModel.DataAnnotations;

namespace AppRoomGameBackend.DTOs.Character
{
    public class CharacterCreateDto
    {
        [Required(ErrorMessage = "Name boş olamaz.")]
        [MinLength(3, ErrorMessage = "Name minimum 3 karakter olmalıdır.")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Level negatif olamaz.")]
        public int Level { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Health negatif olamaz.")]
        public int Health { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Mana negatif olamaz.")]
        public int Mana { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Gold negatif olamaz.")]
        public int Gold { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "XP negatif olamaz.")]
        public int XP { get; set; }
    }
    
    // Id Bulunmuyor
}
