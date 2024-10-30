using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pitstop.RentalCarManagementAPI.Events;
using Pitstop.RentalManagementAPI.DataAccess;
using RentalCarManagementAPI.Models;
using Serilog;

namespace Pitstop.RentalManagementAPI.EventHandlers;

public class RentalCarRegisteredHandler(RentalManagementDBContext dbContext, IMapper mapper) : PitstopEventHandler("RentalCarRegistered", dbContext, mapper)
{
    public override async Task<bool> HandleAsync(JObject e)
    {
        var serialized = e.ToObject<RentalCarRegistered>()!;
        Log.Information("Register Car: {LicenseNumber}",
            serialized.LicenseNumber);

        try
        {
            await DbContext.RentalCars.AddAsync(new RentalCar
            {
                Id = serialized.Id,
                LicenseNumber = serialized.LicenseNumber,
                ModelId = serialized.ModelId,
            });
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding brand with rental car id {rentalCarId}.", serialized.Id);
        }

        return true;
    }
}