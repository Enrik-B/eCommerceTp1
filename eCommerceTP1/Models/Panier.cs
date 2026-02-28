using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    public class Panier
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("clientId")]
        public int clientId { get; set; }
        public User Client { get; set; }
        public ICollection<ProduitPanier> ProduitsPanier { get; set; } = new List<ProduitPanier>();

    }
}
