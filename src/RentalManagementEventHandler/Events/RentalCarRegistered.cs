using System;
using Pitstop.Infrastructure.Messaging;

namespace Pitstop.RentalCarManagementAPI.Events;

public class RentalCarRegistered() : Event(Guid.NewGuid())
{
    public string Id { get; set; }
    public string LicenseNumber { get; set; }
    public string ModelId { get; set; }
}