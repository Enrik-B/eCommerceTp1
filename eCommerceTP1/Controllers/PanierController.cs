using eCommerceTP1.Models;
using eCommerceTP1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace eCommerceTP1.Controllers
{
    public class PanierController : Controller
    {
        private readonly PanierService _panierService;
        private readonly UserService _userService;
        private readonly ProduitService _produitService;
        private readonly eCommerceTP1DbContext _context;

        private User? GetUser() 
        {
            string Id = HttpContext.Session.GetString("UserId") ?? "-1";
            User? user = _userService.GetUserById(int.Parse(Id));
            return user;
        }
        public PanierController(PanierService panierService, UserService userservice, ProduitService produitService, eCommerceTP1DbContext context)
        {
            _panierService = panierService;
            _userService = userservice;
            _produitService = produitService;
            _context = context;
        }
        public IActionResult Index()
        {
            User? user = GetUser();
            if (user == null)
            {
                return NotFound();
            }
            else if (user.Role != "Client")
            {
                return Unauthorized();
            }
            else 
            {
                return View(_context.Paniers.Include(p => p.ProduitsPanier).ThenInclude(pp => pp.Produit).First(p => p.UserId == user.Id));
            }
        }
        // Cette action ne rafraîchit pas la page et ne retourne qu'un JSON
        [HttpPost]
        public IActionResult AddProduitToPanier(int id, bool reloadPage = false)
        {
            User? user = GetUser();
            if (user == null) 
            {
                return NotFound();
            }
            Produit? produit = _produitService.GetProduitById(id);
            if (produit == null) 
            {
                Debug.WriteLine("Produit pas trouvé lors de AddProduitToPanier");
                return NotFound();
            }
            _panierService.AddProduitPanier(user.Panier, produit.Id);
            if (reloadPage == false)
            {
                return Json(new
                {
                    titre = "✅ Nouveau produit!",
                    message = $"{produit.Title} ajouté au panier."
                });
            }
            else 
            {
                return RedirectToAction("Index");
            }

        }
      
       public IActionResult Recap()
        {
            var user = GetUser();
            if (user == null)
            {
                return RedirectToAction("Index", "Panier");
            }

            var panier = _context.Paniers
                .Include(p => p.ProduitsPanier)
                .ThenInclude(pp => pp.Produit)
                .FirstOrDefault(p => p.UserId == user.Id);

            if (panier == null)
            {
                return RedirectToAction("Index", "Panier");
            }

            return View("RecapCommande", panier);
        }
        [HttpPost]
        public IActionResult RemoveProduitFromPanier(int id, bool reloadPage = false) 
        {
            User? user = GetUser();
            if (user == null)
            {
                return NotFound();
            }
            Produit? produit = _produitService.GetProduitById(id);
            if (produit == null)
            {
                Debug.WriteLine("Produit pas trouvé lors de RemoveProduitFromPanier");
                return NotFound();
            }
            bool success = _panierService.SubstractProduitPanier(user.Panier, produit.Id);
            if (success == true) 
            {
                if (reloadPage == false)
                {
                    return Json(new
                    {
                        titre = "Produit enlevé!",
                        message = $"{produit.Title} enlevé du panier"
                    });
                }
                else 
                {
                    return RedirectToAction("Index");
                }
                
            } else 
            { 
                return NotFound(); 
            }
        }
    }
}
