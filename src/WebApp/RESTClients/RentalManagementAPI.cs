namespace WebApp.RESTClients;

public class RentalManagementAPI : IRentalManagementAPI
{
    private IRentalManagementAPI _restClient;

    public RentalManagementAPI(IConfiguration config, HttpClient httpClient)
    {
        string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("RentalManagementAPI");
        httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
        _restClient = RestService.For<IRentalManagementAPI>(
            httpClient,
            new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            });
    }

    public async Task<List<Rental>> GetRentals()
    {
        return await _restClient.GetRentals();
    }
    public async Task<Rental> GetRentalById([AliasAs("id")] string id)
    {
        try
        {
            return await _restClient.GetRentalById(id);
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

    public async Task RegisterRental(RegisterRental command)
    {
        await _restClient.RegisterRental(command);
    }
    
    public async Task ChangeCarRental([AliasAs("id")] string id, ChangeCarRental command)
    {
        await _restClient.ChangeCarRental(id, command);
    }
    
    public async Task ExtendRental([AliasAs("id")] string id, ExtendRental command)
    {
        await _restClient.ExtendRental(id, command);
    }

    public async Task DeleteRental([AliasAs("id")] string id)
    {
        await _restClient.DeleteRental(id);
    }
}