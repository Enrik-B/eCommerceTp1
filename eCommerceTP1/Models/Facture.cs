using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    public class Facture
    {
        [Key]
        public int Id { get; set; }

        public string NumeroFacture { get; set; } = "FAC-" + Guid.NewGuid().ToString().Substring(0, 8);

        public DateTime FactureTime { get; set; } = DateTime.Now;

        public decimal MontantTotal { get; set; }

        public string StatutPaiement { get; set; }  // Payée / Refusée

        public string StripePaymentId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int ClientId { get; set; }
        public int VendeurId { get; set; }

        public ICollection<ProduitFacture> ProduitsFacture { get; set; } = new List<ProduitFacture>();
    }
}
