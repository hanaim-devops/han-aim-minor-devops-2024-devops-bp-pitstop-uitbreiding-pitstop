using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using Pitstop.RentalManagementAPI.DataAccess;
using Pitstop.RentalManagementAPI.Models;
using Pitstop.RentalManagementEventHandler.Events;
using Serilog;

namespace Pitstop.RentalManagementAPI;

public class EventHandlerWorker : IHostedService, IMessageHandlerCallback
{
    RentalManagementDBContext _dbContext;
    IMessageHandler _messageHandler;

    public EventHandlerWorker(IMessageHandler messageHandler, RentalManagementDBContext dbContext)
    {
        _messageHandler = messageHandler;
        _dbContext = dbContext;
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
        try
        {
            switch (messageType)
            {
                case "CustomerRegistered":
                    await HandleAsync(messageObject.ToObject<CustomerRegistered>());
                    break;
            }
        }
        catch (Exception ex)
        {
            string messageId = messageObject.Property("MessageId") != null ? messageObject.Property("MessageId").Value<string>() : "[unknown]";
            Log.Error(ex, "Error while handling {MessageType} message with id {MessageId}.", messageType, messageId);
        }

        // always akcnowledge message - any errors need to be dealt with locally.
        return true;
    }
    
    private async Task<bool> HandleAsync(CustomerRegistered e)
    {
        Log.Information("Register Customer: {CustomerId}, {Name}, {TelephoneNumber}",
            e.CustomerId, e.Name, e.TelephoneNumber);

        try
        {
            await _dbContext.Customers.AddAsync(new Customer
            {
                CustomerId = e.CustomerId,
                Name = e.Name,
                TelephoneNumber = e.TelephoneNumber
            });
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding customer with customer id {CustomerId}.", e.CustomerId);
        }

        return true;
    }
}