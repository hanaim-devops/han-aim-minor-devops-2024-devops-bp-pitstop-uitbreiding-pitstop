using System.ComponentModel.DataAnnotations;

namespace DIYManagementAPI.Models
{
    public class DIYRegistration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DIYEveningId { get; set; }

        [Required]
        public string? CustomerName { get; set; }

        [Required]
        public string? Reparations { get; set; }
    }
}
