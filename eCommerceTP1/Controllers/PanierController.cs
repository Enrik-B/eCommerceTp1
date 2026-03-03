using eCommerceTP1.Models;
using eCommerceTP1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eCommerceTP1.Controllers
{
    public class PanierController : Controller
    {
        private readonly PanierService _panierService;
        private readonly UserService _userService;
        private readonly ProduitService _produitService;

        private User? GetUser() 
        {
            string Id = HttpContext.Session.GetString("UserId") ?? "-1";
            User? user = _userService.GetUserById(int.Parse(Id));
            return user;
        }
        public PanierController(PanierService panierService, UserService userservice, ProduitService produitService)
        {
            _panierService = panierService;
            _userService = userservice;
            _produitService = produitService;
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
                return View(user.Panier);
            }
        }
        // Cette action ne rafraîchit pas la page et ne retourne qu'un JSON
        [HttpPost]
        public IActionResult AddProduitToPanier(int id) 
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
            _panierService.AddProduitPanier(user.Panier, produit);
            return Json(new
            {
                titre = "✅ Nouveau produit!",
                message = $"{produit.GetAPIProduit().Title} ajouté au panier."
            });
        }
        [HttpPost]
        public IActionResult RemoveProduitFromPanier(int id) 
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
            bool success = _panierService.SubstractProduitPanier(user.Panier, produit);
            if (success == true) 
            {
                return Json(new
                {
                    titre = "Produit enlevé!",
                    message = $"{produit.GetAPIProduit().Title} enlevé du panier"
                });
            } else 
            { 
                return NotFound(); 
            }
        }
    }
}
