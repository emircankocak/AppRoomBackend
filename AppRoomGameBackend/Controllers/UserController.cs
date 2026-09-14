using AppRoomGameBackend.Models;
using AppRoomGameBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AppRoomGameBackend.DTOs.User;

using BCrypt.Net;
using Microsoft.Identity.Client;

namespace AppRoomGameBackend.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppRoomGameDbContext _context;

        public UserController(AppRoomGameDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Kullanıcı Başarıyla Oluşturuldu");



        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Username == dto.Username);

            if(user == null)
            {
                return Unauthorized("Kullanıcı adı veya şifre hatalı");
            }

            //şifreyi kontrol et
            bool passwordCorrect = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash);

            //Şifre yanlışsa
            if(!passwordCorrect)
            {
                return Unauthorized("Kullanıcı adı veya şifre hatalı");
            }

            return Ok("Giriş Başarılı");
        }
    }
}
