using eCommerceTP1.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTP1.Services
{
    public class FactureService
    {
        private readonly eCommerceTP1DbContext _context;
        public FactureService(eCommerceTP1DbContext dbContext)
        {
            _context = dbContext;
        }
        public Facture? GetFactureById(int Id) 
        {
            Facture? facture = _context.Factures.Include(f => f.ProduitsFacture).ThenInclude(pf => pf.Produit).FirstOrDefault(f => f.Id == Id);
            return facture;
        }

        public void AddFacture(Facture facture)
        {
            _context.Factures.Add(facture);
            _context.SaveChanges();
        }
        /*
        public List<Facture>? CommandeToFacture(int Id) 
        {
            Commande? commande = _context.Commandes.Include(c => c.commandeProduits).ThenInclude(cp => cp.Produit).FirstOrDefault(c => c.Id == Id);
            // preparer une liste vide de Vendeurs qui s'agrandit selon nombre de vendeurs trouvés à travers tous les produits mentionnés
        }   
        */
    }
}
