using AppRoomGameBackend.Models;
using AppRoomGameBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AppRoomGameBackend.DTOs.Item;

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
                .Include(x => x.Category )
                .Include(x => x.Character)
                .ToList();

            return Ok(item);
        }

        [HttpPost()]
        public IActionResult AddItem([FromBody] ItemCreateDto dto)
        {
            var category = _context.Categories
                .FirstOrDefault(x => x.Id == dto.CategoryId);

            var character = _context.Characters
                .FirstOrDefault(x => x.Id == dto.CharacterId);

            if (category==null || character==null)
            {
                return NotFound("Kategori veya Karakter Bulunamadı!");
            }

            var item = new Item
            {
                Name = dto.Name,
                Type = dto.Type,
                Quantity = dto.Quantity,
                CategoryId = dto.CategoryId,
                CharacterId = character.Id,
                Category = category,
                Character = character
            };

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
            var item = _context.Items
                .Include(x =>x.Category)
                .Include(x =>x.Character)
                .FirstOrDefault(x => x.Id==id);

            if (item == null)
            {
                return NotFound("İtem Bulunamadı!");
            }

            return Ok(item);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] ItemUpdateDto dto)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id == id);

            var category = _context.Categories
                    .FirstOrDefault(x => x.Id == dto.CategoryId);

            // Gönderilen CharacterId'nin veritabanında olup olmadığını kontrol et
            var character = _context.Characters
                .FirstOrDefault(x => x.Id == dto.CharacterId);
            // category ve character nesnesini oluşturmamızın nedeni ,
            // olmayan Id değei ile işlem yapınca 404 Found döndürebilmesi için.
            if (category == null || character == null)
            {
                return NotFound("Kategori veya Karakter Bulunamadı!");
            }

            
            item.Name = dto.Name;
            item.Type = dto.Type;
            item.Quantity = dto.Quantity;
            item.CategoryId = dto.CategoryId;
            item.CharacterId = dto.CharacterId;

        
            _context.SaveChanges();
            return Ok(item);
 
        }
    }
}
