using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pitstop.RentalManagementAPI.Commands;
using Pitstop.RentalManagementAPI.Models;
using Pitstop.RentalManagementAPI.Services.Interfaces;

namespace Pitstop.RentalManagementAPI.Services;

public class RentalPlanningService(RentalManagementDbContext dbContext) : IRentalPlanningService
{
    private RentalManagementDbContext _dbContext = dbContext;
    
    public RentalReservation Make(MakeReservation command)
    {
        var reservation = new RentalReservation
        {
            CarId = command.CarId,
            CustomerId = command.CustomerId,
            StartDate = command.StartDate,
            EndDate = command.EndDate
        };
        _dbContext.RentalReservations.Add(reservation);
        _dbContext.SaveChanges();
        return _dbContext.RentalReservations.Include(r => r.Customer)
            .Include(r => r.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(m => m.Brand)
            .First(r => r.Id == reservation.Id);
    }
    
    public List<RentalReservation> GetAll()
    {
        return _dbContext.RentalReservations
            .Include(r => r.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(m => m.Brand)
            .ToList();
    }
}