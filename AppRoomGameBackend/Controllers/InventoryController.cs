using AppRoomGameBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppRoomGameBackend.Controllers
{
    [ApiController]
    [Route("api/Inventories")]
    public class InventoryController : ControllerBase
    {
        private static Character character = new Character
        {
            Id = 1,
            Name = "Warrior",
            Level = 5,
            Health = 100,
            Mana = 50,
            Gold = 500
        };



        [HttpPost()]
        public IActionResult AddItem([FromBody] Item item)
        {
            character.Inventory.Add(item);

            return Ok(item);
        }


        [HttpDelete("{id}")]
        public IActionResult RemoveItem(int id)
        {
            var item = character.Inventory.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound("İtem Bulunamadı!");
            }

            character.Inventory.Remove(item);

            return Ok("İtem silindi.");

        }


        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            var item = character.Inventory.FirstOrDefault(x => x.Id==id);

            if (item == null)
            {
                return NotFound("İtem Bulunamadı!");
            }

            return Ok(item);
        }


        [HttpPut("{id}")]
        public IActionResult PutItem(int id, [FromBody] Item UpdatedItem)
        {
            var item = character.Inventory.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound("İtem BUlunamdı");
            }

            item.Name = UpdatedItem.Name;
            item.Type = UpdatedItem.Type;
            item.Quantity = UpdatedItem.Quantity;

            return Ok(item);
        }
    }
}
