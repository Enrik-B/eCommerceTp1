using eCommerceTP1.Models;

namespace eCommerceTP1.Services
{
    public class FactureService
    {
        private readonly eCommerceTP1DbContext _dbContext;
        public FactureService(eCommerceTP1DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Facture? GetFactureById(int Id) 
        { 
            Facture? facture = _dbContext.Factures.Find(Id);
            return facture;
        }

        public void AddFacture(Facture facture)
        {
            _dbContext.Factures.Add(facture);
            _dbContext.SaveChanges();
        }
    }
}
