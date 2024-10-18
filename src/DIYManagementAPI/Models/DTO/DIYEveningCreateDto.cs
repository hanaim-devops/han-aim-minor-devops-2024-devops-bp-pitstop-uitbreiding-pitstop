using System.ComponentModel.DataAnnotations;

namespace DIYManagementAPI.Models.DTO
{
    public class DIYEveningCreateDto
    {
        [Required]
        public required string Title { get; set; }

        public required string ExtraInfo { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public required string Mechanic { get; set; }

    }
}
