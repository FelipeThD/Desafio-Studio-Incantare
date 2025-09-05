using System.ComponentModel.DataAnnotations;

namespace BackendTraining.Models
{
    public class ContactsModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Message { get; set; } = string.Empty;
    }
}
