namespace WebApp.RESTClients;

public interface IRentalManagementAPI
{
    [Get("/rentalplanning")]
    Task<List<Rental>> GetRentals();

    [Get("/rentalplanning/{id}")]
    Task<Rental> GetRentalByLicenseNumber([AliasAs("id")] string licenseNumber);

    [Post("/rentalplanning")]
    Task RegisterRental(RegisterRental command);
}