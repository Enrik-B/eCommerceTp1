using eCommerceTP1.Models;
using eCommerceTP1.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceTP1.Controllers
{
    public class DashboardVendeurController : Controller
    {
        private readonly eCommerceTP1DbContext _context;
        private readonly UserService _userService;
        private User? GetUser()
        {
            string Id = HttpContext.Session.GetString("UserId") ?? "-1";
            User? user = _userService.GetUserById(int.Parse(Id));
            return user;
        }
        public DashboardVendeurController(eCommerceTP1DbContext context, UserService userService)
        {
            _userService = userService;
            _context = context;
        }
        public IActionResult Index()
        {
            User? user = GetUser();
            ViewBag.Produits = _context.Produits.Where(p => p.VendeurId == user.Id).Count();
            ViewBag.Factures = _context.Factures.Where(f => f.VendeurId == user.Id).Count();
            ViewBag.Ventes = Math.Round(_context.Factures
                .Where(f => f.VendeurId == user.Id)
                .Sum(f => f.ProduitsFacture
                .Sum(pf => pf.Quantite * pf.Produit.Price)),2);
            return View();
        }
    }
}
