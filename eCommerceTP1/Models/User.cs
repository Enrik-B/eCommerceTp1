using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTP1.Models
{
    
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le prénom est requis.")]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Le nom de famille est requis.")]
        [Display(Name = "Nom de famille")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Le genre est requis.")]
        [Display(Name = "Genre")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "Adresse e-mail invalide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone est requis.")]
        [Phone(ErrorMessage = "Numéro de téléphone invalide.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La date de naissance est requise.")]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Le pays est requis.")]
        [Display(Name = "Pays")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Le rôle est requis.")]
        public string Role { get; set; } // "Client" ou "Vendeur"
        public ICollection<Facture> Factures { get; set; } = new List<Facture>();
    }
}
