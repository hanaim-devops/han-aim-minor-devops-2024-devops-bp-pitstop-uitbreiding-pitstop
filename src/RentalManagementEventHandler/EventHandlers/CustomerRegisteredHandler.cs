using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pitstop.RentalManagementAPI.DataAccess;
using Pitstop.RentalManagementAPI.Models;
using Pitstop.RentalManagementEventHandler.Events;
using Serilog;

namespace Pitstop.RentalManagementAPI.EventHandlers;

public class CustomerRegisteredHandler(RentalManagementDBContext dbContext, IMapper mapper) : PitstopEventHandler("CustomerRegistered", dbContext, mapper)
{
    public override async Task<bool> HandleAsync(JObject e)
    {
        var serialized = e.ToObject<CustomerRegistered>()!;
        Log.Information("Register Customer: {CustomerId}, {Name}, {TelephoneNumber}",
            serialized.CustomerId, serialized.Name, serialized.TelephoneNumber);

        try
        {
            await DbContext.Customers.AddAsync(new Customer
            {
                CustomerId = serialized.CustomerId,
                Name = serialized.Name,
                TelephoneNumber = serialized.TelephoneNumber
            });
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding customer with customer id {CustomerId}.", serialized.CustomerId);
        }

        return true;
    }
}