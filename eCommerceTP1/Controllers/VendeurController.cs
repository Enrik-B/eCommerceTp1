using eCommerceTP1.Models;
using eCommerceTP1.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceTP1.Controllers
{
    public class VendeurController : Controller
    {
        private readonly eCommerceTP1DbContext _context;
        private readonly UserService _userService;
        private User? GetUser()
        {
            string Id = HttpContext.Session.GetString("UserId") ?? "-1";
            User? user = _userService.GetUserById(int.Parse(Id));
            return user;
        }
        public VendeurController(eCommerceTP1DbContext context, UserService userService)
        {
            _userService = userService;
            _context = context;
        }
        public IActionResult Produits()
        {
            User? user = GetUser();
            if (user == null) 
            {
                return Unauthorized();
            }
            List<Produit> produits = _context.Produits.Where(p => p.VendeurId == user.Id).ToList();
            return View(produits.OrderBy(p => p.Id));
        }
        
      
       [HttpGet]
public IActionResult AddProduit()
{
    return View();
}

[HttpPost]
        [HttpPost]
        public IActionResult AddProduit(Produit produit, IFormFile imageFile)
        {
            var user = GetUser();
            if (user == null)
                return Unauthorized();

            // Gestion de l'image
            if (imageFile != null && imageFile.Length > 0)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                produit.Image = "/images/" + fileName; 
            }
            else
            {
                produit.Image = "/images/default.png";
            }

            produit.VendeurId = user.Id;

            _context.Produits.Add(produit);
            _context.SaveChanges();

            return RedirectToAction("Produits");
        }
    }
}
