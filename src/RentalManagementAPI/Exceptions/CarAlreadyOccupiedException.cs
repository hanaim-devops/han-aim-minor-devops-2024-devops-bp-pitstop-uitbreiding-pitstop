using System;

namespace Pitstop.RentalManagementAPI.Exceptions;

public class CarAlreadyOccupiedException(DateTime startDate, DateTime endDate) : Exception("Car already occupied. Between " + startDate + " and " + endDate)
{
    
}