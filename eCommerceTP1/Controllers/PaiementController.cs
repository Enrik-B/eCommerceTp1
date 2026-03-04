using eCommerceTP1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System.Diagnostics;

namespace eCommerceTP1.Controllers
{
    public class PaiementController : Controller
    {
        private readonly StripeSettings _stripeSettings;
        public PaiementController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
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
