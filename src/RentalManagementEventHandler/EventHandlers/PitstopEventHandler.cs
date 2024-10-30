using AutoMapper;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using Pitstop.RentalManagementAPI.DataAccess;

namespace Pitstop.RentalManagementAPI.EventHandlers;

public abstract class PitstopEventHandler(string messageType, RentalManagementDBContext dbContext, IMapper mapper)
{
    public string MessageType { get; } = messageType;
    protected RentalManagementDBContext DbContext = dbContext;
    public abstract Task<bool> HandleAsync(JObject e); 
}