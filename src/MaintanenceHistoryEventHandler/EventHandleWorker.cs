using System.Diagnostics;
using Pitstop.MaintanenceHistoryEventHandler.DataAccess;
using Pitstop.MaintanenceHistoryEventHandler.Events;
using Pitstop.MaintanenceHistoryEventHandler.Model;

namespace Pitstop.MaintanenceHistoryEventHandler;

public class EventHandleWorker(IMessageHandler messageHandler, MaintenanceHistoryContext DBcontext) : IHostedService, IMessageHandlerCallback
{

    public Task StartAsync(CancellationToken cancellationToken)
    {
        messageHandler.Start(this);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        messageHandler.Stop();
        return Task.CompletedTask;
    }

    public async Task<bool> HandleMessageAsync(string messageType, string message)
    {
        var messageObject = MessageSerializer.Deserialize(message);
        
        try
        {
            switch (messageType)
            {
                case "MaintenanceHistory":
                    return await HandleAsync(messageObject.ToObject<HistoryAdded>());
                    break;
            }
        }
        catch (Exception e)
        {
            string messageId = messageObject.Property("MessageId") != null ? messageObject.Property("MessageId").Value<string>() : "[unknown]";
            Log.Error(e, "Error while handling {MessageType} message with id {MessageId}.", messageType, messageId);
        }
        
        return true;
    }
    
    private Task<bool> HandleAsync(HistoryAdded maintenanceJobPlanned)
    {
        var maintenanceJob = new MaintenanceHistory
        {
            LicenseNumber = maintenanceJobPlanned.LicenseNumber,
            MaintenanceDate = maintenanceJobPlanned.MaintenanceDate,
            Description = maintenanceJobPlanned.Description,
            MaintenanceJobId = maintenanceJobPlanned.MaintenanceJobId
        };
        
        DBcontext.MaintenanceHistories.Add(maintenanceJob);
        DBcontext.SaveChanges();
        
        return Task.FromResult(true);
    }
}