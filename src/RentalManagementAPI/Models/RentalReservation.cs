using System;
using RentalManagementAPI.Models;

namespace Pitstop.RentalManagementAPI.Models;

public class RentalReservation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public RentalCar Car { get; set; }
    public string CarId { get; set; }
    public Customer Customer { get; set; }
    public string CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}