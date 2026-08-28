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
