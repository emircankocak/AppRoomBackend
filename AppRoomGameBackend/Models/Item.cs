using System.Text.Json.Serialization;

namespace AppRoomGameBackend.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }

        //Category ---> Items
        public int CategoryId { get; set; } 
        public Category? Category { get; set; }

        //Character ---> Items
        public int? CharacterId { get; set; }
        public Character? Character { get; set; }
    }
}
