namespace WebApp.RESTClients;

public interface IRentalManagementAPI
{
    [Get("/rentalplanning")]
    Task<List<Rental>> GetRentals();

    [Get("/rentalplanning/{id}")]
    Task<Rental> GetRentalById([AliasAs("id")] string id);

    [Post("/rentalplanning")]
    Task RegisterRental(RegisterRental command);
    
    [Patch("/rentalplanning/{id}/car")]
    Task ChangeCarRental([AliasAs("id")] string id, ChangeCarRental command);
    
    [Patch("/rentalplanning/{id}/extend")]
    Task ExtendRental([AliasAs("id")] string id, ExtendRental command);
    
    [Delete("/rentalplanning/{id}")]
    Task DeleteRental([AliasAs("id")] string id);
}