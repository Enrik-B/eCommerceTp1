using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    public class Client : User
    {
        public int PanierId { get; set; }
        public Panier Panier { get; set; }
        // Factures dans User
    }
}
