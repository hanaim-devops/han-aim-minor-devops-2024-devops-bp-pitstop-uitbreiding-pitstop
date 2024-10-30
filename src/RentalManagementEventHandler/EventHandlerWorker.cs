using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using Pitstop.RentalManagementAPI.DataAccess;
using Pitstop.RentalManagementAPI.EventHandlers;
using Pitstop.RentalManagementAPI.Models;
using Pitstop.RentalManagementEventHandler.Events;
using Serilog;

namespace Pitstop.RentalManagementAPI;

public class EventHandlerWorker : IHostedService, IMessageHandlerCallback
{
    RentalManagementDBContext _dbContext;
    IMessageHandler _messageHandler;

    private IEnumerable<PitstopEventHandler> _handlers;

    private PitstopEventHandler GetHandlerFor(string messageType)
    {
        return _handlers.First(h => h.MessageType == messageType);
    }

    public EventHandlerWorker(IMessageHandler messageHandler, RentalManagementDBContext dbContext, IEnumerable<PitstopEventHandler> handlers)
    {
        _messageHandler = messageHandler;
        _dbContext = dbContext;
        _handlers = handlers;
    }

    public void Start()
    {
        _messageHandler.Start(this);
    }

    public void Stop()
    {
        _messageHandler.Stop();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _messageHandler.Start(this);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _messageHandler.Stop();
        return Task.CompletedTask;
    }

    public async Task<bool> HandleMessageAsync(string messageType, string message)
    {
        JObject messageObject = MessageSerializer.Deserialize(message);
        var handler = GetHandlerFor(messageType);
        await handler.HandleAsync(messageObject);
        return true;
    }
}