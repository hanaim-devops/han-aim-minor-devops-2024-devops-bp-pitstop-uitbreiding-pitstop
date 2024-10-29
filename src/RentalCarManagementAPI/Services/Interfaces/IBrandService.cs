using RentalCarManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.Services.Interfaces;

public interface IBrandService
{
    public Brand GetByName(string name);
}