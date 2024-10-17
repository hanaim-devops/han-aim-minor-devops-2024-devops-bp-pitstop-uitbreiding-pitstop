using MaintenanceHistoryAPI.Enums;

namespace Pitstop.WorkshopManagementEventHandler;

public class EventHandlerWorker(
    IMessageHandler messageHandler,
    WorkshopManagementDBContext workshopManagementDbContext,
    MaintenanceHistoryContext maintenanceHistoryContext)
    : IHostedService, IMessageHandlerCallback
{
    public void Start()
    {
        messageHandler.Start(this);
    }

    public void Stop()
    {
        messageHandler.Stop();
    }

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
        JObject messageObject = MessageSerializer.Deserialize(message);
        try
        {
            switch (messageType)
            {
                case "CustomerRegistered":
                    await HandleAsync(messageObject.ToObject<CustomerRegistered>());
                    break;
                case "VehicleRegistered":
                    await HandleAsync(messageObject.ToObject<VehicleRegistered>());
                    break;
                case "MaintenanceJobPlanned":
                    await HandleAsync(messageObject.ToObject<MaintenanceJobPlanned>());
                    break;
                case "MaintenanceJobFinished":
                    await HandleAsync(messageObject.ToObject<MaintenanceJobFinished>());
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

    private async Task<bool> HandleAsync(VehicleRegistered e)
    {
        Log.Information("Register Vehicle: {LicenseNumber}, {Brand}, {Type}, Owner Id: {OwnerId}",
            e.LicenseNumber, e.Brand, e.Type, e.OwnerId);

        try
        {
            await workshopManagementDbContext.Vehicles.AddAsync(new Vehicle
            {
                LicenseNumber = e.LicenseNumber,
                Brand = e.Brand,
                Type = e.Type,
                OwnerId = e.OwnerId
            });
            await workshopManagementDbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Console.WriteLine($"Skipped adding vehicle with license number {e.LicenseNumber}.");
        }

        return true;
    }

    private async Task<bool> HandleAsync(CustomerRegistered e)
    {
        Log.Information("Register Customer: {CustomerId}, {Name}, {TelephoneNumber}",
            e.CustomerId, e.Name, e.TelephoneNumber);

        try
        {
            await workshopManagementDbContext.Customers.AddAsync(new Customer
            {
                CustomerId = e.CustomerId,
                Name = e.Name,
                TelephoneNumber = e.TelephoneNumber
            });
            await workshopManagementDbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding customer with customer id {CustomerId}.", e.CustomerId);
        }

        return true;
    }

    private async Task<bool> HandleAsync(MaintenanceJobPlanned e)
    {
        Log.Information("Register Maintenance Job: {JobId}, {StartTime}, {EndTime}, {CustomerName}, {LicenseNumber}",
            e.JobId, e.StartTime, e.EndTime, e.CustomerInfo.Name, e.VehicleInfo.LicenseNumber);

        try
        {
            // determine customer
            Customer customer =
                await workshopManagementDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == e.CustomerInfo.Id);
            if (customer == null)
            {
                customer = new Customer
                {
                    CustomerId = e.CustomerInfo.Id,
                    Name = e.CustomerInfo.Name,
                    TelephoneNumber = e.CustomerInfo.TelephoneNumber
                };
            }

            // determine vehicle
            Vehicle vehicle =
                await workshopManagementDbContext.Vehicles.FirstOrDefaultAsync(v =>
                    v.LicenseNumber == e.VehicleInfo.LicenseNumber);
            if (vehicle == null)
            {
                vehicle = new Vehicle
                {
                    LicenseNumber = e.VehicleInfo.LicenseNumber,
                    Brand = e.VehicleInfo.Brand,
                    Type = e.VehicleInfo.Type,
                    OwnerId = customer.CustomerId
                };
            }

            // insert maintetancejob
            await workshopManagementDbContext.MaintenanceJobs.AddAsync(new MaintenanceJob
            {
                Id = e.JobId,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Customer = customer,
                Vehicle = vehicle,
                WorkshopPlanningDate = e.StartTime.Date,
                Description = e.Description
            });
            await workshopManagementDbContext.SaveChangesAsync();

            // insert maintenance history

            var history = new MaintenanceHistory()
            {
                LicenseNumber = e.VehicleInfo.LicenseNumber,
                MaintenanceDate = e.StartTime.Date,
                MaintenanceType = MaintenanceTypes.Overig,
                MaintenanceJobId = e.JobId,
            };
            await maintenanceHistoryContext.MaintenanceHistories.AddAsync(history);

            Log.Information(
                "Maintenance history added: {LicenseNumber}, {MaintenanceDate}, {MaintenanceType}, {MaintenanceJobId}, {Description}, {IsCompleted}",
                history.LicenseNumber, history.MaintenanceDate, history.MaintenanceType, history.MaintenanceJobId,
                history.Description, history.IsCompleted);

            await maintenanceHistoryContext.SaveChangesAsync();


        }
        catch (DbUpdateException ex)
        {
            Log.Warning("Skipped adding maintenance job with id {JobId}.", e.JobId);
        }
        

        return true;
    }

    private async Task<bool> HandleAsync(MaintenanceJobFinished e)
    {
        Log.Information("Finish Maintenance job: {JobId}, {ActualStartTime}, {EndTime}",
            e.JobId, e.StartTime, e.EndTime);

        try
        {
            // insert maintetancejob
            var job = await workshopManagementDbContext.MaintenanceJobs.FirstOrDefaultAsync(j => j.Id == e.JobId);
            job.ActualStartTime = e.StartTime;
            job.ActualEndTime = e.EndTime;
            job.Notes = e.Notes;
            await workshopManagementDbContext.SaveChangesAsync();
            
            // insert maintenance history
            var maintenanceHistory = await maintenanceHistoryContext.MaintenanceHistories.FirstOrDefaultAsync(m => m.MaintenanceJobId == e.JobId);
            maintenanceHistory.MaintenanceDate = e.StartTime.Date;
            maintenanceHistory.Description = e.Notes;
            maintenanceHistory.MaintenanceType = MaintenanceTypes.Overig;
            maintenanceHistory.IsCompleted = true;
            await maintenanceHistoryContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Log.Warning("Skipped adding maintenance job with id {JobId}.", e.JobId);
        }

        return true;
    }
}