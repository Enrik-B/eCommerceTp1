using eCommerceTP1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTP1.Controllers
{
    public class InscriptionController : Controller
    {
        private readonly eCommerceTP1DbContext _context;

        public InscriptionController(eCommerceTP1DbContext context) 
        {

            _context = context; 
        }
        public IActionResult Inscription() 
        { return View(); }

        [HttpPost]
        [HttpPost] public IActionResult Register(User user) 
        { 
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); 
            _context.Users.Add(user); 
            _context.SaveChanges(); 
            return RedirectToAction("Login"); 
        }


    }
}
