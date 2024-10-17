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
    
    public async Task<List<MaintenanceHistory>> GetHistoryByLicenseNumber(string licenseNumber)
    {
        //TODO: Status code check naar try verplaatsen zodat niet elke vehicle aan knop krijgt
        try
        {
            var response = await _restClient.GetHistoryByLicenseNumber(licenseNumber);
            return response;
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