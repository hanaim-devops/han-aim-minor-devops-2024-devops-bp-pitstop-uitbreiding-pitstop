using System.Diagnostics;

namespace Pitstop.MaintenanceHistoryAPI;

public class EventHandlerHistory(IMessageHandler messageHandler) : IHostedService, IMessageHandlerCallback
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var activitySource = new ActivitySource("Pitstop.MaintenanceHistoryAPI.EventHandlerHistory");
        messageHandler.Start(this, activitySource);
        
        Console.WriteLine("EventHandlerHistory started.");
        
        await Task.CompletedTask;
        
        Console.WriteLine("EventHandlerHistory started.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        messageHandler.Stop();
        return Task.CompletedTask;
    }

    public async Task<bool> HandleMessageAsync(string messageType, string message)
    {
        try
        {
            var messageObject = MessageSerializer.Deserialize(message);
            switch (messageType)
            {
                case "MaintenanceHistory":
                    await HandleAsync(messageObject.ToObject<MaintenanceHistory>());
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Error while handling {messageType} event.");
        }

        return true;
    }
    
    private Task HandleAsync(MaintenanceHistory history)
    {
        Log.Information("Maintenance history: {JobId}, {JobDescription}, {PlannedMaintenanceDate}, {ActualMaintenanceDate}, {Status}",
            history.MaintenanceJobId, history.Description, history.MaintenanceDate, DateTime.Now, "Overig");
        
        return Task.CompletedTask;
    }
}