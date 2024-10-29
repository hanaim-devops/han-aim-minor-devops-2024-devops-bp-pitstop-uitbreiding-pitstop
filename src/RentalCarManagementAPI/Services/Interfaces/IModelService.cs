using RentalCarManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.Services.Interfaces;

public interface IModelService
{
    public Model GetByName(string brand, string name);
}