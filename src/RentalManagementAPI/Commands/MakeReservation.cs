using System;

namespace Pitstop.RentalManagementAPI.Commands;

public class MakeReservation
{
    public string CarId { get; set; }
    public string CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}