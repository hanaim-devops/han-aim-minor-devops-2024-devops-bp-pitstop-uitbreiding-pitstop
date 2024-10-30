using System;
using Pitstop.RentalManagementAPI.Models;
using RentalManagementAPI.Models;

namespace Pitstop.RentalManagementAPI.Responses;

public class RentalReservationResponse
{
    public string Id { get; set; }
    public RentalCarResponse Car { get; set; }
    public CustomerResponse Customer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}