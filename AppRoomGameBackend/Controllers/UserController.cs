using AppRoomGameBackend.Data;
using AppRoomGameBackend.DTOs.User;
using AppRoomGameBackend.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace AppRoomGameBackend.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppRoomGameDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(AppRoomGameDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            string passwordHash = 
                BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = dto.Role
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

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };


            var key = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(
                 _configuration["Jwt:Key"]!
                 )
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new
            {
                message = "Giriş başarılı",
                token = tokenString
            });
        }

        [Authorize] //Bu attribute endpoint'in JWT authentication gerektirdiğini belirtir
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return Ok("Bu endpoint sadece giriş yapan kullanıcılar içindir");
        }
    }
}
