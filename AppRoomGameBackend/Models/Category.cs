using System.Text.Json.Serialization;

namespace AppRoomGameBackend.Models
{
    public class Category
    {
        public string? Name { get; set; }

        public int Id { get; set; }

        [JsonIgnore]
        public List<Item>? Items { get; set; }
    }
}