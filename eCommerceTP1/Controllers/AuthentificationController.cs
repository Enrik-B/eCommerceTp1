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
            if (user.Role == "Client") {
                Panier panier = new Panier();
                user.Panier = panier;
                _context.Paniers.Add(panier);
            }
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

        //Afficher les données des utilisateurs 
        public async Task<IActionResult> Profile()
        {
            ModelState.Clear();
            var userIdString = HttpContext.Session.GetString("UserId");


            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Index", "Home");

            int userId = int.Parse(userIdString);

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound();

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Index", "Home");

            int userId = int.Parse(userIdString);

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.Country = model.Country;
            user.BirthDate = model.BirthDate;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Profil mis à jour !";

            return RedirectToAction("Profile");
        }
    }
}   

