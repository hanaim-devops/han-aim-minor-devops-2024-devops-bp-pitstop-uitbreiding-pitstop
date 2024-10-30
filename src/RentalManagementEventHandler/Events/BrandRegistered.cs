using System;
using Pitstop.Infrastructure.Messaging;

namespace Pitstop.RentalCarManagementAPI.Events;

public class BrandRegistered() : Event(Guid.NewGuid())
{
    public string Id { get; set; }
    public string Name { get; set; }
}