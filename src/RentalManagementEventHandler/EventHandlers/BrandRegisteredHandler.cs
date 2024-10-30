using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pitstop.RentalCarManagementAPI.Events;
using Pitstop.RentalManagementAPI.DataAccess;
using RentalCarManagementAPI.Models;
using Serilog;

namespace Pitstop.RentalManagementAPI.EventHandlers;

public class BrandRegisteredHandler(RentalManagementDBContext dbContext, IMapper mapper) : PitstopEventHandler("BrandRegistered", dbContext, mapper)
{
    public override async Task<bool> HandleAsync(JObject e)
    {
        var serialized = e.ToObject<BrandRegistered>()!;
        Log.Information("Register Brand: {Name}",
            serialized.Name);

        try
        {
            await DbContext.Brands.AddAsync(new Brand
            {
                Id = serialized.Id,
                Name = serialized.Name,
            });
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding brand with brand id {brand}.", serialized.Id);
        }

        return true;
    }
}