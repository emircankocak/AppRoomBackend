using AppRoomGameBackend.Models;
using AppRoomGameBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace AppRoomGameBackend.Controllers
{
    [ApiController]
    [Route("api/Inventories")]
    public class InventoryController : ControllerBase
    {
        private readonly AppRoomGameDbContext _context;

        public InventoryController(AppRoomGameDbContext context)
        {
            _context = context;
        }


        private static Character character = new Character
        {
            Id = 1,
            Name = "Warrior",
            Level = 5,
            Health = 100,
            Mana = 50,
            Gold = 500
        };
        private static List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Sword", Type = "Weapon", Quantity = 1 },
            new Item { Id = 2, Name = "Shield", Type = "Armor", Quantity = 1 },
            new Item { Id = 3, Name = "Health Potion", Type = "Consumable", Quantity = 5 }
        };



        [HttpPost()]
        public IActionResult AddItem([FromBody] Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();

            return Ok(item);
        }


        [HttpDelete("{id}")]
        public IActionResult RemoveItem(int id)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound("İtem Bulunamadı!");
            }

            _context.Items.Remove(item);
            _context.SaveChanges();

            return Ok("İtem silindi.");

        }


        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id==id);

            if (item == null)
            {
                return NotFound("İtem Bulunamadı!");
            }

            return Ok(item);
        }


        [HttpPut("{id}")]
        public IActionResult PutItem(int id, [FromBody] Item UpdatedItem)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound("İtem Bulunamadı!");
            }

            item.Name = UpdatedItem.Name;
            item.Type = UpdatedItem.Type;
            item.Quantity = UpdatedItem.Quantity;

            _context.SaveChanges();
            return Ok(item);
        }
    }
}
