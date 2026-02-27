using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    [NotMapped]
    public abstract class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Facture> Factures { get; set; } = new List<Facture>();
    }
}
