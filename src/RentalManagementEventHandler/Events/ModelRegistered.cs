using System;
using Pitstop.Infrastructure.Messaging;

namespace Pitstop.RentalCarManagementAPI.Events;

public class ModelRegistered() : Event(Guid.NewGuid())
{
    public string Id { get; set; }
    public string BrandId { get; set; }
    public string Name { get; set; }
}