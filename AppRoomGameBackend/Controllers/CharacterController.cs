using AppRoomGameBackend.Data;
using AppRoomGameBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AppRoomGameBackend.DTOs.Character;

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


        [HttpGet("{id} ")]
        public IActionResult GetCharacter(int id)
        {
            var character = _context.Characters
                .Include(x => x.Items)
                .FirstOrDefault(x => x.Id==id);

            if(character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        //Test amaçlı hata fırlatma
        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            throw new Exception("Test amaçlı hata");
        }

        [HttpPost]
        public IActionResult AddCharacters([FromBody] 
        CharacterCreateDto dto)                                                              // [FromBody] ile API isteğinin gövdesinden gelen karakter oluşturma
                                                                             // verileri CharacterCreateDto nesnesine aktarılır
        {
            Character character = new Character
            {
                Name = dto.Name,
                Level = dto.Level,
                Health = dto.Health,
                Mana = dto.Mana,
                Gold = dto.Gold,
                XP = dto.XP
            };
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
        public IActionResult UpdateCharacter(int id, [FromBody] 
        CharacterUpdateDto dto)
        {
            var character = _context.Characters
                .FirstOrDefault(x => x.Id==id);

            if(character == null)
            {
                return NotFound("İşlem Bulunamadı!");
            }

            character.Name = dto.Name;
            character.Level= dto.Level;
            character.Health = dto.Health;
            character.Mana = dto.Mana;
            character.Gold = dto.Gold;
            character.XP = dto.XP;

            _context.SaveChanges();
            return Ok(character);
        }
    }
}
