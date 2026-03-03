using eCommerceTP1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
<<<<<<< HEAD
=======
using eCommerceTP1.Models;
using eCommerceTP1.Services;
using System.Diagnostics;
>>>>>>> 049b9b5441a7dfba026659897023a396999d3d8a

namespace eCommerceTP1.Controllers
{
    public class DashboardClientController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly eCommerceTP1DbContext _context;

        public DashboardClientController(eCommerceTP1DbContext context)
        {
            _httpClient = new HttpClient();
            _context = context;
        }

        private async Task FetchProduits() 
        {
            var response = await _httpClient.GetStringAsync("https://dummyjson.com/products");
            var produits = JsonSerializer.Deserialize<ProduitResponse>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Debug.WriteLine("RÉCUPÉRATION DES PRODUITS: "+produits.Products.Count());
            ProduitResponseGlobal.Products = produits.Products;
        }

        public async Task<IActionResult> Index(string search = "", string category = "")
        {
            if (ProduitResponseGlobal.Products == null || ProduitResponseGlobal.Products.Count == 0) 
            {
                await FetchProduits();
            }
            List<ProduitAPI> produits = ProduitResponseGlobal.Products;
            // Filtrage
            if (!string.IsNullOrEmpty(search))
            {
                produits = produits.Where(p =>
                    p.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(category))
            {
                produits = produits.Where(p =>
                    p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Liste des catégories pour le dropdown
            ViewBag.Categories = produits?.Select(p => p.Category).Distinct().ToList();

            return View(produits);
        }
        //Afficher les détails d'un produit 
        public async Task<IActionResult> DetailProduit(int id)
        {
            if (ProduitResponseGlobal.Products == null || ProduitResponseGlobal.Products.Count == 0) 
            {
                await FetchProduits();
            }

            ProduitAPI? produit = ProduitResponseGlobal.Products.Find(p => p.Id == id);
            if (produit == null) 
            {
                return NotFound();
            }

            return View(produit);
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