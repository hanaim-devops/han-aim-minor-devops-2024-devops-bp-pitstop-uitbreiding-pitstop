using System.Collections.Generic;
using Pitstop.RentalCarManagementAPI.Commands;
using RentalCarManagementAPI;
using RentalCarManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.Services.Interfaces;

public interface IRentalCarService
{
    public RentalCar Add(RegisterRentalCar rentalCar);
    public List<RentalCar> GetAll();
    public RentalCar GetByLicenseNumber(string id);
    void DeleteRentalCar(string rentalCarId);
}