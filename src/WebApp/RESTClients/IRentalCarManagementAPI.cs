namespace WebApp.RESTClients;

public interface IRentalCarManagementAPI
{
    [Get("/rentalcars")]
    Task<List<RentalCar>> GetRentalCars();

    [Get("/rentalcars/{id}")]
    Task<RentalCar> GetRentalCarById([AliasAs("id")] string id);

    [Post("/rentalcars")]
    Task RegisterRentalCar(RegisterRentalCar command);
}