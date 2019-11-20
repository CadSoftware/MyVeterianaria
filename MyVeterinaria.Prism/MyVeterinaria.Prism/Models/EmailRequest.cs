using System.ComponentModel.DataAnnotations;

namespace MyVeterinaria.Prism.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
