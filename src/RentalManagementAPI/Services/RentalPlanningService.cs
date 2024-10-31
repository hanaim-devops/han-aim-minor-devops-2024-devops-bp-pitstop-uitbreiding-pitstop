using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using Pitstop.RentalManagementAPI.Commands;
using Pitstop.RentalManagementAPI.Exceptions;
using Pitstop.RentalManagementAPI.Extensions;
using Pitstop.RentalManagementAPI.Models;
using Pitstop.RentalManagementAPI.Services.Interfaces;

namespace Pitstop.RentalManagementAPI.Services;

public class RentalPlanningService(RentalManagementDbContext dbContext, IMapper mapper) : IRentalPlanningService
{
    private RentalManagementDbContext _dbContext = dbContext;
    private IMapper _mapper = mapper;
    
    public List<RentalReservation> GetAll()
    {
        return _dbContext.RentalReservations
            .Include(r => r.Customer)
            .Include(r => r.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(m => m.Brand)
            .ToList();
    }
    
    public RentalReservation Make(MakeReservation command)
    {
        var reservation = _mapper.Map<RentalReservation>(command);
        
        if (command.EndDate.IsBefore(command.StartDate)) throw new EndDateIsBeforeStartDateException();
        if (command.StartDate.IsBefore(DateTime.Now)) throw new DateIsInThePastException();
        
        if (AlreadyOccupied(command.CarId, command.StartDate, command.EndDate, reservation.Id)) throw new CarAlreadyOccupiedException(command.StartDate, command.EndDate);
        
        _dbContext.RentalReservations.Add(reservation);
        _dbContext.SaveChanges();
        
        return _dbContext.RentalReservations
            .Include(r => r.Customer)
            .Include(r => r.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(m => m.Brand)
            .First(r => r.Id == reservation.Id);
    }

    public RentalReservation GetById(string id)
    {
        var reservation = _dbContext.RentalReservations
            .Include(r => r.Customer)
            .Include(r => r.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(m => m.Brand)
            .FirstOrDefault(r => r.Id == id);
        if (reservation == null) throw new NotFoundException("Reservation");
        return reservation;
    }

    public RentalReservation ChangeCar(string id, ChangeCar command)
    {
        var reservation = _dbContext.RentalReservations
            .Include(r => r.Customer)
            .Include(r => r.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(m => m.Brand)
            .First(r => r.Id == id);
        
        if (AlreadyOccupied(command.CarId, reservation.StartDate, reservation.EndDate, reservation.Id)) throw new CarAlreadyOccupiedException(reservation.StartDate, reservation.EndDate);
        
        reservation.CarId = command.CarId;
        _dbContext.SaveChanges();
        return reservation;
    }

    public RentalReservation Extend(string id, Extend command)
    {
        var reservation = _dbContext.RentalReservations
            .Include(r => r.Customer)
            .Include(r => r.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(m => m.Brand)
            .First(r => r.Id == id);
        
        if (command.EndDate.IsBefore(reservation.StartDate)) throw new EndDateIsBeforeStartDateException();
        if (command.EndDate.IsBefore(DateTime.Now)) throw new DateIsInThePastException();
        
        if (AlreadyOccupied(reservation.CarId, reservation.StartDate, reservation.EndDate, reservation.Id)) throw new CarAlreadyOccupiedException(reservation.StartDate, reservation.EndDate);
        
        reservation.EndDate = command.EndDate;
        _dbContext.SaveChanges();
        return reservation;
    }

    private bool AlreadyOccupied(string carId, DateTime startDate, DateTime endDate, string id = "")
    {
        var reservations = _dbContext.RentalReservations
            .Where(r => r.CarId == carId && (r.Id != id || id.IsNullOrEmpty()))
            .ToList();

        return reservations.Any(r => r.StartDate.IsBetween(startDate, endDate) || r.EndDate.IsBetween(startDate, endDate));
    }
}