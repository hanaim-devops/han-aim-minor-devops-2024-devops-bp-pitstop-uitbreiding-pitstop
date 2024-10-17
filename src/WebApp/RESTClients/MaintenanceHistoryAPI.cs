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

    public async Task<MaintenanceHistory> GetHistoryById(int id)
    {
        try
        {
            return await _restClient.GetHistoryById(id);
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

    public async Task<List<MaintenanceHistory>> GetHistoryByLicenseNumber(string licenseNumber)
    {
        try
        {
            return await _restClient.GetHistoryByLicenseNumber(licenseNumber);
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