using System.Collections.Generic;
using Pitstop.RentalManagementAPI.Commands;
using Pitstop.RentalManagementAPI.Models;

namespace Pitstop.RentalManagementAPI.Services.Interfaces;

public interface IRentalPlanningService
{
    public RentalReservation Make(MakeReservation command);
    public List<RentalReservation> GetAll();
}