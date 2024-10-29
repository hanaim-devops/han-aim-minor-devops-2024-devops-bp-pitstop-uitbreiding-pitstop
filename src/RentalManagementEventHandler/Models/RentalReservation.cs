using System;
using RentalCarManagementAPI.Models;

namespace Pitstop.RentalManagementAPI.Models;

public class RentalReservation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public Customer Customer { get; set; }
    
    public string CustomerId { get; set; }
    
    public RentalCar RentalCar { get; set; }
    
    public string RentalCarId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
}