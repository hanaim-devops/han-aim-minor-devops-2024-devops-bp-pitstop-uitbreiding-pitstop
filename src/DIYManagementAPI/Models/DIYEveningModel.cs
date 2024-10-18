using System.ComponentModel.DataAnnotations;

namespace DIYManagementAPI.Models
{
    public class DIYEveningModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string ExtraInfo { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public required string Mechanic { get; set; }

    }
}
