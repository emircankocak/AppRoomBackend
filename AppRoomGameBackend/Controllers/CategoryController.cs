using AppRoomGameBackend.Data;
using AppRoomGameBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AppRoomGameBackend.DTOs.Category;
using Microsoft.AspNetCore.Authorization;
namespace AppRoomGameBackend.Controllers
{
    [ApiController]
    [Route("api/Categories")]
    public class CategoryController : ControllerBase
    {
        private readonly AppRoomGameDbContext _context;

        public CategoryController(AppRoomGameDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetCategories()
        {
            var category = _context.Categories.ToList();

            return Ok(category);
        }


        
        [HttpGet("{id}")]
        public IActionResult GetCategories(int id)
        {
            var category = _context.Categories.Find(id);

            return Ok(category);
        }


        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult AddCategory( [FromBody] CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return Ok(category);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id,[FromBody] CategoryUpdateDto dto)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return NotFound("İşlem Bulunamadı!");
            }

            category.Name = dto.Name;
            _context.SaveChanges();

            return Ok(category);

        }


        [HttpDelete("{id}")]
        public IActionResult RemoveCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id==id);

            if(category ==null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok("Silme İşlemi Başarılı");

        }

    }
}
