using eCommerceTP1.Models;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using System.Diagnostics;
using System.Text.Json;
=======
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using eCommerceTP1.Services;
using System.Diagnostics;
>>>>>>> 5e46e0ccc953a4c46aea9c001c70729fdd0eb34c

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

    }
}