
namespace WebApp.RESTClients;

public class DIYManagementAPI : IDIYManagementAPI
{
    private IDIYManagementAPI _restClient;

    public DIYManagementAPI(IConfiguration config, HttpClient httpClient)
    {
        string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("CustomerManagementAPI");
        httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
        _restClient = RestService.For<IDIYManagementAPI>(
            httpClient,
            new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            });
    }

    public async Task<DIYAvond> GetDIYAvondById([AliasAs("id")] int diyEveningId)
    {
        return await _restClient.GetDIYAvondById(diyEveningId);
    }

    public async Task<List<DIYAvond>> GetDIYAvonden()
    {
        return await _restClient.GetDIYAvonden();
    }

    public async Task RegisterDIYAvondCustomer(RegisterDIYRegistration command)
    {
        await _restClient.RegisterDIYAvondCustomer(command);
    }
}