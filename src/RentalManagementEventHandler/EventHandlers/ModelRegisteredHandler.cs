using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pitstop.RentalCarManagementAPI.Events;
using Pitstop.RentalManagementAPI.DataAccess;
using Pitstop.RentalManagementAPI.Models;
using RentalCarManagementAPI.Models;
using Serilog;

namespace Pitstop.RentalManagementAPI.EventHandlers;

public class ModelRegisteredHandler(RentalManagementDBContext dbContext, IMapper mapper) : PitstopEventHandler("ModelRegistered", dbContext, mapper)
{
    public override async Task<bool> HandleAsync(JObject e)
    {
        var serialized = e.ToObject<ModelRegistered>()!;
        Log.Information("Register Model: {BrandId}, {Name}",
            serialized.BrandId, serialized.Name);

        try
        {
            await DbContext.Models.AddAsync(new Model
            {
                Id = serialized.Id,
                BrandId = serialized.BrandId,
                Name = serialized.Name,
            });
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding model with model id {ModelId}.", serialized.Id);
        }

        return true;
    }
}