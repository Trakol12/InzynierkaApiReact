using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InzynierkaApiReact.Server.Data;
using InzynierkaApiReact.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace InzynierkaApiReact.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User model)
        {
            var user = _context.User.SingleOrDefault(u => u.UserName == model.UserName);
            if (user == null)
            {
                return Unauthorized(new { message = "Nieprawidłowa nazwa użytkownika lub hasło." });
            }
            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Nieprawidłowa nazwa użytkownika lub hasło." });
            }
            // Zapis danych użytkownika w sesji
            

            return Ok(new { message = "Zalogowano pomyślnie", isAdmin = user.IsAdmin });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User model)
        {         
            if(model.Password == string.Empty || model.UserName == string.Empty) 
            { 
                return BadRequest(new {message = "Nie podano nazwy użytkownika bądź hasłą"});
            }

            // Sprawdzenie, czy użytkownik już istnieje
            if (_context.User.Any(u => u.UserName == model.UserName))
            {
                return BadRequest(new { message = "Użytkownik o takiej nazwie już istnieje." });
            }

            var passwordHasher = new PasswordHasher<User>();
            model.Password = passwordHasher.HashPassword(model, model.Password); // Hashowanie hasła
            
            // Dodanie nowego użytkownika
            _context.User.Add(model);
            _context.SaveChanges();
            return Ok(new { message = "Konto zostało utworzone pomyślnie." });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {           
            return Ok(new { message = "Wylogowano pomyślnie." });
        }
    }
    }

