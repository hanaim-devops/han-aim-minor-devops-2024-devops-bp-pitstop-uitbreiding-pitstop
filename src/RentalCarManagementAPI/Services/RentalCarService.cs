using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pitstop.Infrastructure.Messaging;
using Pitstop.RentalCarManagementAPI.Commands;
using Pitstop.RentalCarManagementAPI.Events;
using Pitstop.RentalCarManagementAPI.Exceptions;
using Pitstop.RentalCarManagementAPI.Services.Interfaces;
using RentalCarManagementAPI;
using RentalCarManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.Services;

public class RentalCarService(RentalCarManagementDBContext dbContext, IModelService modelService, IMessagePublisher publisher, IMapper mapper) : IRentalCarService
{
    private RentalCarManagementDBContext _dbContext = dbContext;
    private IModelService _modelService = modelService;
    private IMessagePublisher _publisher = publisher;
    private IMapper _mapper = mapper;
    
    public RentalCar Add(RegisterRentalCar command)
    {
        var existingCar = _dbContext.RentalCars.FirstOrDefault(c => c.LicenseNumber == command.LicenseNumber);
        if (existingCar != null) throw new LicensePlateAlreadyRegistered();
        
        var model = _modelService.GetByName(command.Brand, command.Model);
        var rentalCar = new RentalCar()
        {
            Id = Guid.NewGuid().ToString(),
            LicenseNumber = command.LicenseNumber,
            Model = model,
        };
        
        _dbContext.RentalCars.Add(rentalCar);
        _dbContext.SaveChanges();
        var @event = _mapper.Map<RentalCarRegistered>(rentalCar);
        _publisher.PublishMessageAsync(@event.MessageType, @event, "");
        
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

    public void DeleteRentalCar(string rentalCarId)
    {
        var reservation = _dbContext.RentalCars.FirstOrDefault(r => r.Id == rentalCarId);
        if (reservation == null) throw new NotFoundException();
        _dbContext.RentalCars.Remove(reservation);
        _dbContext.SaveChanges();
    }
}