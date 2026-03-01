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

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetStringAsync("https://dummyjson.com/products");

            var data = JsonSerializer.Deserialize<ProduitResponse>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Conversion DummyProduct → Produit
            var produits = data?.Products.Select(p => new Produit
            {
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category,
                Image = p.Images?.FirstOrDefault()
            }).ToList();

            return View(produits);
        }
    }
}