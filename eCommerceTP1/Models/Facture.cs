using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    public class Facture
    {
        [Key]
        public int Id { get; set; }
       
        public int ClientId { get; set; }      public int VendeurId { get; set; }
        public int userId { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }
        public ICollection<ProduitFacture> ProduitsFacture { get; set; } = new List<ProduitFacture>();
    }
}
