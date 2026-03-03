using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    [NotMapped]
    public class ProduitAPI
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public string Category { get; set; }
        // Lien URL à l'image
        public string Image { get; set; }
        public List<string> Images { get; set; }
    }
    public static class ProduitResponseGlobal
    {
        public static List<ProduitAPI>? Products { get; set; }
    }
    public class ProduitResponse 
    {
        public List<ProduitAPI>? Products { get; set; }
    }

}
