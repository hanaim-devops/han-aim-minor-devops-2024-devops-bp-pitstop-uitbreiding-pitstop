namespace WebApp.RESTClients;

public class RentalCarManagementAPI : IRentalCarManagementAPI
{
    private IRentalCarManagementAPI _restClient;

    public RentalCarManagementAPI(IConfiguration config, HttpClient httpClient)
    {
        string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("RentalCarManagementAPI");
        httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
        _restClient = RestService.For<IRentalCarManagementAPI>(
            httpClient,
            new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            });
    }

    public async Task<List<RentalCar>> GetRentalCars()
    {
        return await _restClient.GetRentalCars();
    }
    public async Task<RentalCar> GetRentalCarByLicenseNumber([AliasAs("id")] string licenseNumber)
    {
        try
        {
            return await _restClient.GetRentalCarByLicenseNumber(licenseNumber);
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

    public async Task RegisterRentalCar(RegisterRentalCar command)
    {
        await _restClient.RegisterRentalCar(command);
    }
}