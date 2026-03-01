using eCommerceTP1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTP1.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly eCommerceTP1DbContext _context;

        public AuthentificationController(eCommerceTP1DbContext context)
        {

            _context = context;
        }
        public IActionResult Inscription()
        { return View(); }

        [HttpPost]
        public IActionResult Inscription(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Login() { return View(); }
        [HttpPost]
        public IActionResult Login(string Identifier, string Password)
        {
            var user = _context.Users.FirstOrDefault(u => (u.Email == Identifier || u.Username == Identifier));
            if (user == null)
            {
                ViewBag.Error = "Utilisateur introuvable.";
                return View();
            }
            bool passwordOk = BCrypt.Net.BCrypt.Verify(Password, user.Password);
            if (!passwordOk)
            {
                ViewBag.Error = "Mot de passe incorrect.";
                return View();
            }
            // Stockage en session
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);
            // Redirection selon le rôle
            if (user.Role == "Client")
            {
                return RedirectToAction("Index", "DashboardClient");
            }
            else if (user.Role == "Vendeur")
            {
                return RedirectToAction("Index", "DashboardVendeur");
            }
            return RedirectToAction("Index", "Home");
        } 
    }

}   

