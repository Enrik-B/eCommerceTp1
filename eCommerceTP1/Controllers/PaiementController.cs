using eCommerceTP1.Models;
using eCommerceTP1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using System.Diagnostics;
using System.Security.Claims;

namespace eCommerceTP1.Controllers
{
    public class PaiementController : Controller
    {
        private readonly StripeSettings _stripeSettings;
        private readonly eCommerceTP1DbContext _context;
        private readonly PanierService _panierService;
        public PaiementController(IOptions<StripeSettings> stripeSettings, eCommerceTP1DbContext context, PanierService panierService)
        {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            _context = context;
            _panierService = panierService;
        }
        public IActionResult Index()
        {
            ViewBag.StripePublicKey = _stripeSettings.PublishableKey;
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Payer(string stripeToken)
        {

            if (string.IsNullOrEmpty(stripeToken))
            {
                return RedirectToAction("Refuse");
            }

            try
            {

                var options = new ChargeCreateOptions
                {
                    Amount = 5000,
                    Currency = "eur",
                    Source = stripeToken,
                    Description = "Paiement commande"
                };

                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);


                if (charge.Status == "succeeded")
                {
                    string id = HttpContext.Session.GetString("UserId");
                    int userId = int.Parse(id);

                    // Récupérer le panier de l'utilisateur
                    var panier = _context.Paniers
                        .Include(p => p.ProduitsPanier)
                        .ThenInclude(pp => pp.Produit)
                        .FirstOrDefault(p => p.UserId == userId);

                    if (panier == null)
                    {
                        return RedirectToAction("Refuse");
                    }
                    // Grouper les produits par vendeur
                    var GroupeProduits = panier.ProduitsPanier.GroupBy(pp => pp.Produit.VendeurId);
                    foreach (var groupe in GroupeProduits) 
                    {
                        decimal total = groupe.Sum(pp => pp.Produit.Price * pp.Quantite);
                        var facture = new Facture
                        {
                            UserId = userId,
                            ClientId = userId,
                            VendeurId = groupe.Key,
                            MontantTotal = total,
                            StatutPaiement = "Payée",
                            StripePaymentId = charge.Id,
                            ProduitsFacture = new List<ProduitFacture>()
                        };
                        foreach (ProduitPanier pp in groupe)
                        {
                            facture.ProduitsFacture.Add(new ProduitFacture()
                            {
                                ProduitId = pp.ProduitId,
                                Quantite = pp.Quantite
                            });
                        }
                        _context.Factures.Add(facture);
                    }
                    panier.ProduitsPanier.Clear();
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Succes");
                }

                return RedirectToAction("Refuse");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Refuse");
            }
        }
        public IActionResult Succes()
        {
            return View();
        }

        public IActionResult Refuse()
        {
            return View();
        }
    }
}
