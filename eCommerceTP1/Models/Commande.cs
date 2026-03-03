namespace eCommerceTP1.Models
{
    public class Commande
    {
        public int Id { get; set; }

        public int userId { get; set; }
        public User User { get; set; }
        public ICollection<CommandeProduit> commandeProduits { get; set; }
    }
}
