namespace WebApp.RESTClients;

public class MaintenanceHistoryAPI : IMaintenanceHistoryAPI
{
    private IMaintenanceHistoryAPI _restClient;
    
    public MaintenanceHistoryAPI(IConfiguration config, HttpClient httpClient)
    {
        string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("MaintenanceHistoryAPI");
        httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
        _restClient = RestService.For<IMaintenanceHistoryAPI>(
            httpClient,
            new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            });
    }
    
    public async Task<MaintenanceHistory> GetHistoryById(string licenseNumber)
    {
        //TODO: Status code check naar try verplaatsen
        try
        {
            return await _restClient.GetHistoryById(licenseNumber);
        }
        catch (ApiException ex)
        {
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw;
            }
        }
    }
}