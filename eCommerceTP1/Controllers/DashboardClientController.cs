using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using eCommerceTP1.Models;

namespace eCommerceTP1.Controllers
{
    public class DashboardClientController : Controller
    {
        private readonly HttpClient _httpClient;

        public DashboardClientController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index(string search = "", string category = "")
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
                Price = p.Price,
                Category = p.Category,
                Image = p.Images?.FirstOrDefault()
            }).ToList();

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
        public async Task<IActionResult> DetailProduit(int id)
        {
            var response = await _httpClient.GetStringAsync($"https://dummyjson.com/products/{id}");

            var data = JsonSerializer.Deserialize<Produit>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data == null)
                return NotFound();

            // Mapping vers le modèle produit
            var produit = new Produit
            {
                Id = data.Id,
                Title = data.Title,
                Description = data.Description,
                Price = data.Price,
                Category = data.Category,
                Image = data.Images?.FirstOrDefault()
            };

            return View(produit);
        }
    }
}