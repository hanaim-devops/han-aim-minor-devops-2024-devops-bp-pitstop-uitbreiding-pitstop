namespace WebApp.RESTClients;

public interface IRentalCarManagementAPI
{
    [Get("/rentalcars")]
    Task<List<RentalCar>> GetRentalCars();

    [Get("/rentalcars/{id}")]
    Task<RentalCar> GetRentalCarByLicenseNumber([AliasAs("id")] string licenseNumber);

    [Post("/rentalcars")]
    Task RegisterRentalCar(RegisterRentalCar command);
}