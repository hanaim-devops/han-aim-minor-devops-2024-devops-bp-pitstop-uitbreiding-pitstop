using System;

namespace Pitstop.RentalManagementAPI.Exceptions;

public class NotFoundException(string entity) : Exception(entity + " not found.")
{
    
}