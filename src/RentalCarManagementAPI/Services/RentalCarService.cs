using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pitstop.RentalCarManagementAPI.Commands;
using Pitstop.RentalCarManagementAPI.Exceptions;
using Pitstop.RentalCarManagementAPI.Services.Interfaces;
using RentalCarManagementAPI;
using RentalCarManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.Services;

public class RentalCarService(RentalCarManagementDBContext dbContext, IModelService modelService) : IRentalCarService
{
    private RentalCarManagementDBContext _dbContext = dbContext;
    private IModelService _modelService = modelService;
    
    public RentalCar Add(RegisterRentalCar command)
    {
        var model = _modelService.GetByName(command.Brand, command.Model);
        var rentalCar = new RentalCar()
        {
            Id = Guid.NewGuid().ToString(),
            LicenseNumber = command.LicenseNumber,
            Model = model,
        };
        
        _dbContext.RentalCars.Add(rentalCar);
        _dbContext.SaveChanges();
        return rentalCar;
    }

    public List<RentalCar> GetAll()
    {
        return _dbContext.RentalCars.Include(c => c.Model)
            .ThenInclude(m => m.Brand)
            .ToList();
    }

    public RentalCar GetByLicenseNumber(string licenseNumber)
    {
        var car = _dbContext.RentalCars.Include(c => c.Model)
            .ThenInclude(m => m.Brand)
            .FirstOrDefault(c => c.LicenseNumber == licenseNumber);
        if (car != null) return car;
        throw new NotFoundException();
    }
}