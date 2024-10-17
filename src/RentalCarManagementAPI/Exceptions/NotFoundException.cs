using System;

namespace Pitstop.RentalCarManagementAPI.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Entity Not Found.")
    {
    }
}