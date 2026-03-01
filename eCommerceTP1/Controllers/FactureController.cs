using eCommerceTP1.Models;
using eCommerceTP1.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceTP1.Controllers
{
    [Route("Factures")]
    public class FactureController : Controller
    {
        private readonly FactureService _factureService;
        public FactureController(FactureService factureService) 
        { 
            _factureService = factureService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("{Id}")]
        public IActionResult Details(int Id) 
        {
            Facture? facture = _factureService.GetFactureById(Id);
            if (facture != null)
            {
                return View("FactureDetail", facture);
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
