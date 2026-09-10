namespace AppRoomGameBackend.DTOs.Item
{
    public class ItemUpdateDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public int? CharacterId { get; set; }
    }
}
