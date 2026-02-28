using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    public class ProduitPanier
    {
        [Key]
        public int Id { get; set; }
        public int ProduitId { get; set; }
        public Produit Produit { get; set; }
        public int Quantite { get; set; }
        [ForeignKey("PanierId")]
        public int PanierId { get; set; }
        public Panier Panier { get; set; }
    }
}
