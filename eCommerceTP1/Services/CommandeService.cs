using eCommerceTP1.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTP1.Services
{
    public class CommandeService
    {
        private readonly eCommerceTP1DbContext _dbContext;
        public CommandeService(eCommerceTP1DbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public Commande CreerCommande(Panier panier, int userId)
        {
            var commande = new Commande
            {
                userId = userId,
                commandeProduits = new List<CommandeProduit>()
            };

            foreach (var item in panier.ProduitsPanier)
            {
                commande.commandeProduits.Add(new CommandeProduit
                {
                    ProduitId = item.ProduitId,
                    Quantite = item.Quantite
                });
            }

            _dbContext.Commandes.Add(commande);
            _dbContext.SaveChanges();

            return commande;
        }

        public void ConfirmerCommande(int commandeId)
        {
            var commande = _dbContext.Commandes
                .Include(c => c.commandeProduits)
                .FirstOrDefault(c => c.Id == commandeId);

            if (commande == null)
                return;

            _dbContext.SaveChanges();
        }

    }
}
