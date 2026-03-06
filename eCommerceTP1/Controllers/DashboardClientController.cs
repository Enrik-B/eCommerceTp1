using eCommerceTP1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        // Initialiser les produits de l'API dans la base de données
        private async Task FetchProduits()
        {
            var response = await _httpClient.GetStringAsync("https://dummyjson.com/products");
            var data = JsonSerializer.Deserialize<ProduitResponse>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            var produits = data?.Products.Select(p => new Produit
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = Math.Round(p.Price, 2),
                Category = p.Category,
                Image = p.Images?.FirstOrDefault(),
                Images = p.Images,
                VendeurId = p.Id < 15 ? 1 : 2
            }).ToList();
            await _context.Produits.AddRangeAsync(produits);
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> Index(string search = "", string category = "")
        {
            if (_context.Produits.Any() == false)
            {
                await FetchProduits(); // Retrouve 30 produits de l'API pour initialiser une collection de produits
            }
            List<Produit> produits = _context.Produits.ToList();
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
            ViewBag.Categories = produits?.Select(p => p.Category).Distinct().ToList();

            return View(produits);
        }
        //Afficher les détails d'un produit 
        public async Task<IActionResult> DetailProduit(int id)
        {
            if (_context.Produits.Any() == false) 
            {
                await FetchProduits();
            }

            Produit? produit = _context.Produits.Find(id);
            if (produit == null) 
            {
                return NotFound();
            }

            return View(produit);
        }

    }
}