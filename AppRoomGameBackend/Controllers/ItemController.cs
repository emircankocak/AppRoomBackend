using AppRoomGameBackend.Models;
using AppRoomGameBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppRoomGameBackend.Controllers
{
    [ApiController]
    [Route("api/Items")]
    public class ItemController : ControllerBase
    {
        private readonly AppRoomGameDbContext _context;

        public ItemController(AppRoomGameDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult GetItems()
        {
            var item = _context.Items
                .Include(x => x.Category)
                .ToList();

            return Ok(item);
        }

        [HttpPost()]
        public IActionResult AddItem([FromBody] Item item)
        {
            var category = _context.Categories
                .FirstOrDefault(x => x.Id == item.CategoryId);

            if (category==null)
            {
                return NotFound("İşlem Bulunamadı!");
            }

            _context.Items.Add(item);
            _context.SaveChanges();

            item.Category = category;

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
            var item = _context.Items
                .Include(x =>x.Category)
                .FirstOrDefault(x => x.Id==id);

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
