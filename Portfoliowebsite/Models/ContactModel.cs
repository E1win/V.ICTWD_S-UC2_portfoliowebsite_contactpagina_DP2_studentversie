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
        [RegularExpression("(?:[a-z0-9!#$%&'*+\\x2f=?^_`\\x7b-\\x7d~\\x2d]+(?:\\.[a-z0-9!#$%&'*+\\x2f=?^_`\\x7b-\\x7d~\\x2d]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9\\x2d]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9\\x2d]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9\\x2d]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])", ErrorMessage = "Vul een geldig emailadres in.")]
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
