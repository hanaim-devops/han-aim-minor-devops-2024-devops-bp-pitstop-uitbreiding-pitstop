using System.Collections.Generic;
using Pitstop.RentalManagementAPI.Commands;
using Pitstop.RentalManagementAPI.Models;

namespace Pitstop.RentalManagementAPI.Services.Interfaces;

public interface IRentalPlanningService
{
    public RentalReservation Make(MakeReservation command);
    public RentalReservation GetById(string id);
    public RentalReservation ChangeCar(string id, ChangeCar command);
    public RentalReservation Extend(string id, Extend command);
    public List<RentalReservation> GetAll();
}