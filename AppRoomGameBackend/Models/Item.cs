using System.Text.Json.Serialization;

namespace AppRoomGameBackend.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }


        public int CategoryId { get; set; } 
      
        public Category? Category { get; set; }
    }
}
