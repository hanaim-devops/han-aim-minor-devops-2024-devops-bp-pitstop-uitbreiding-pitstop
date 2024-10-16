namespace Pitstop.WorkshopManagementAPI.CommandHandlers;

public interface IAddHistoryMaintenanceCommandHandler
{
    Task<MaintenanceHistory> HandleCommandAsync(PlanMaintenanceJob command);
}