using eCommerceTP1.Models;
using eCommerceTP1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTP1.Controllers
{
    public class FactureController : Controller
    {
        private readonly FactureService _factureService;
        private readonly UserService _userService;
        private readonly eCommerceTP1DbContext _context;
        public FactureController(FactureService factureService, UserService userService, eCommerceTP1DbContext context) 
        {
            _userService = userService;
            _factureService = factureService;
            _context = context;
        }
        private User? GetUser()
        {
            string Id = HttpContext.Session.GetString("UserId") ?? "-1";
            User? user = _userService.GetUserById(int.Parse(Id));
            return user;
        }
        [HttpGet]
        public IActionResult Index()
        {
            User? User = GetUser();
            List<Facture> factures = _context.Factures.Include(f => f.ProduitsFacture).ThenInclude(f => f.Produit).Where(f => f.userId == User.Id).ToList();
            return View(factures.OrderBy(f => f.Id));
        }
        public IActionResult FactureDetail(int Id) 
        {
            Facture? facture = _factureService.GetFactureById(Id);
            if (facture != null && GetUser() != null)
            {
                
                return View(facture);
            }
            else 
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult AddFacture(Facture facture) 
        { 
            _factureService.AddFacture(facture);
            return Ok();
        }
    }
}
