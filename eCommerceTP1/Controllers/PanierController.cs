using eCommerceTP1.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceTP1.Controllers
{
    public class PanierController : Controller
    {
        private readonly PanierService _panierService;
        public PanierController(PanierService panierService)
        {
            _panierService = panierService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
