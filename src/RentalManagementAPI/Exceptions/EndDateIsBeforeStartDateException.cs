using System;

namespace Pitstop.RentalManagementAPI.Exceptions;

public class EndDateIsBeforeStartDateException() : Exception("End date is before start date")
{
    
}