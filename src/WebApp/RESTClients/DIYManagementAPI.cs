
namespace WebApp.RESTClients;

public class DIYManagementAPI : IDIYManagementAPI
{
    private IDIYManagementAPI _restClient;

    public DIYManagementAPI(IConfiguration config, HttpClient httpClient)
    {
        string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("DIYManagementAPI");
        httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
        _restClient = RestService.For<IDIYManagementAPI>(
            httpClient,
            new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            });
    }

    public async Task<DIYEvening> GetDIYEveningById([AliasAs("id")] string diyEveningId)
    {
        return await _restClient.GetDIYEveningById(diyEveningId);
    }

    public async Task<List<DIYEvening>> GetDIYEvening()
    {
        return await _restClient.GetDIYEvening();
    }

    public async Task RegisterDIYAvondCustomer(RegisterDIYRegistration command)
    {
        await _restClient.RegisterDIYAvondCustomer(command);
    }
    public async Task RegisterDIYEvening(RegisterDIYEvening cmd)
    {
        await _restClient.RegisterDIYEvening(cmd);
    }
}