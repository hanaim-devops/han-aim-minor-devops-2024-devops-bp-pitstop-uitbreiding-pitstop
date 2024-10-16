namespace Pitstop.WorkshopManagementAPI.CommandHandlers;

public class AddHistoryMaintenanceCommandHandler(IMessagePublisher messageHandler) : IAddHistoryMaintenanceCommandHandler
{
    public async Task<MaintenanceHistory> HandleCommandAsync(PlanMaintenanceJob command)
    {
        // create new maintenance history
        var history = new MaintenanceHistory(command.MessageId)
        {
            Description = command.Description,
            LicenseNumber = command.VehicleInfo.LicenseNumber,
            MaintenanceDate = command.StartTime,
            MaintenanceJobId = command.JobId
        };

        // Event
        await messageHandler.PublishMessageAsync("MaintenanceHistory", history, "");

        // return result
        return history;
    }

}