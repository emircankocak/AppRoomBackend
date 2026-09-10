namespace AppRoomGameBackend.DTOs.Character
{
    public class CharacterUpdateDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Gold { get; set; }
        public int XP { get; set; }
    } // Güncelleme işlemi için Id bulunmuyor çünkü güncelleme işlemi genellikle var olan bir karakterin bilgilerini değiştirmek için kullanılır ve karakterin Id'si zaten biliniyor olmalıdır.
      // Bu nedenle, güncelleme DTO'sunda Id alanına ihtiyaç yoktur.
}
