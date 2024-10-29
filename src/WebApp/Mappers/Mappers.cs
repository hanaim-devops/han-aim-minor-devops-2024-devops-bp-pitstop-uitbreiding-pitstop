namespace Pitstop.WebApp.Mappers;

public static class Mappers
{
    public static RegisterCustomer MapToRegisterCustomer(this CustomerManagementNewViewModel source) => new RegisterCustomer
    (
        Guid.NewGuid(),
        Guid.NewGuid().ToString("N"),
        source.Customer.Name,
        source.Customer.Address,
        source.Customer.PostalCode,
        source.Customer.City,
        source.Customer.TelephoneNumber,
        source.Customer.EmailAddress
    );

    public static RegisterVehicle MapToRegisterVehicle(this VehicleManagementNewViewModel source) => new RegisterVehicle(
        Guid.NewGuid(),
        source.Vehicle.LicenseNumber,
        source.Vehicle.Brand,
        source.Vehicle.Type,
        source.SelectedCustomerId
    );
    
    public static RegisterRentalCar MapToRegisterRentalCar(this RentalCarManagementNewViewModel source) => new RegisterRentalCar(
        Guid.NewGuid(),
        source.RentalCar.LicenseNumber,
        source.RentalCar.Model.Brand.Name,
        source.RentalCar.Model.Name
    );
    
    public static CreateReview MapToCreateReview(this ReviewManagementNewViewModel source) => new CreateReview(
        source.SelectedCustomer,  
        source.ReviewComment,
        source.Stars                
    );
}