using System.ComponentModel.DataAnnotations;

namespace Portfoliowebsite.Models
{
    public class ContactModel
    {
        [Display(Name = "Voor- en achternaam")]
        [Required(ErrorMessage = "Vul uw voor- en achternaam in.")]
        [StringLength(80, ErrorMessage = "Maximaal {1} tekens.")]
        // Unicode letters + spaties (accenten inbegrepen via \p{L})
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$", ErrorMessage = "Alleen letters en spaties zijn toegestaan.")]
        public string? Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vul uw emailadres in.")]
        [StringLength(255, ErrorMessage = "Maximaal {1} tekens.")]
        [RegularExpression("/^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$/", ErrorMessage = "Vul een geldig emailadres in.")]
        public string? Email { get; set; }

        [Display(Name = "Onderwerp")]
        [Required(ErrorMessage = "Vul een onderwerp in.")]
        [StringLength(100, ErrorMessage = "Het onderwerp mag niet langer dan {1} tekens zijn.")]
        public string? Subject { get; set; }

        [Display(Name = "Bericht")]
        [Required(ErrorMessage = "Vul een bericht in.")]
        [StringLength(65536, ErrorMessage = "Maximaal {1} tekens.")]
        public string? Message { get; set; } // todo: dit en andere velden moeten wel gesanitized worden van XSS.

        // Honeypot veld
        public string? Website { get; set; }
    }
}
