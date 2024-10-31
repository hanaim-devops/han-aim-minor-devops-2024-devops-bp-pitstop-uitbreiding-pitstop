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
    public async Task<RentalCar> GetRentalCarById([AliasAs("id")] string id)
    {
        try
        {
            return await _restClient.GetRentalCarById(id);
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

    public async Task DeleteReview(string rentalCarId)
    {
        await _restClient.DeleteReview(rentalCarId);
    }
}