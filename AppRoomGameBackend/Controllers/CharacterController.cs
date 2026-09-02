using AppRoomGameBackend.Data;
using AppRoomGameBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppRoomGameBackend.Controllers
{
    [ApiController]
    [Route("api/Characters")]
    public class CharacterController : ControllerBase
    {
        private readonly AppRoomGameDbContext _context;

        public CharacterController(AppRoomGameDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCharacters()
        {
            var characters = _context.Characters.ToList();

            return Ok(characters);
        }


        [HttpGet("{id}")]
        public IActionResult GetCharacter(int id)
        {
            var character = _context.Characters.FirstOrDefault(x => x.Id==id);

            if(character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }


        [HttpPost]
        public IActionResult AddCharacters([FromBody] Character character)
        {
            _context.Characters.Add(character);
            _context.SaveChanges();

            return Ok(character);
        }

        [HttpDelete]
        public IActionResult DeleteCharacters(int id)
        {
            var character = _context.Characters.FirstOrDefault(x => x.Id==id);

            if(character == null)
            {
                return NotFound("İşlem Bulunamadı!");
            }

            _context.Characters.Remove(character);
            _context.SaveChanges();

            return Ok("Silme İşlemi Başarılı");
        }

        [HttpPut("{id}")]
        public IActionResult PutCharacter(int id, [FromBody] Character UpdatedCharacter)
        {
            var character = _context.Characters.FirstOrDefault(x => x.Id==id);

            if(character == null)
            {
                return NotFound("İşlem Bulunamadı!");
            }

            character.Name = UpdatedCharacter.Name;
            character.Level= UpdatedCharacter.Level;
            character.Health = UpdatedCharacter.Health;
            character.Mana = UpdatedCharacter.Mana;
            character.Gold = UpdatedCharacter.Gold;
            character.XP = UpdatedCharacter.XP;

            _context.SaveChanges();
            return Ok(character);
        }
    }
}
