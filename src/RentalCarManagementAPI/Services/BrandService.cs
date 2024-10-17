using System;
using System.Linq;
using Pitstop.RentalCarManagementAPI.Services.Interfaces;
using RentalCarManagementAPI;
using RentalCarManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.Services;

public class BrandService(RentalCarManagementDBContext dbContext) : IBrandService
{
    private RentalCarManagementDBContext _dbContext = dbContext;
    
    public Brand GetByName(string name)
    {
        var brand = _dbContext.Brands.FirstOrDefault(b => b.Name == name);
        return brand ?? Add(name);
    }
    
    private Brand Add(string name)
    {
        var brand = new Brand()
        {
            Id = Guid.NewGuid().ToString(),
            Name = name
        };
        
        _dbContext.Brands.Add(brand);
        _dbContext.SaveChanges();
        return brand;
    }
}