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

    public Task<DIYAvond> GetDIYAvondById([AliasAs("id")] string diyAvondId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DIYAvond>> GetDIYAvonden()
    {
        return await _restClient.GetDIYAvonden();
    }
}