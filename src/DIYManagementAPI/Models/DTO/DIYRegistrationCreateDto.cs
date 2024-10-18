namespace DIYManagementAPI.Models.DTO
{
    public class DIYRegistrationCreateDto
    {
        public int DIYEveningId { get; set; }

        public required string CustomerName { get; set; }

        public required string Reparations { get; set; }
    }
}
