using System.ComponentModel.DataAnnotations;

namespace DIYManagementAPI.Models
{
    public class DiyTestModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
