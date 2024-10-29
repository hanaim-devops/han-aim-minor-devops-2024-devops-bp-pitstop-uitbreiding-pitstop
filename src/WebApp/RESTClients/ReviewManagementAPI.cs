using Pitstop.WebApp.RESTClients;

namespace WebApp.RESTClients
{
    public class ReviewManagementAPI : IReviewManagementAPI
    {
        private IReviewManagementAPI _restClient;

        public ReviewManagementAPI(IConfiguration config, HttpClient httpClient)
        {
            // Haal de API URL en poort op uit de configuratie
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("ReviewManagementAPI");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
            
            // Initialiseer de Refit client
            _restClient = RestService.For<IReviewManagementAPI>(
                httpClient,
                new RefitSettings
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer()
                });
        }

        // Haal een lijst van alle reviews op
        public async Task<List<Review>> GetReviews()
        {
            return await _restClient.GetReviews();
        }

        // Haal een review op basis van ID
        public async Task<Review> GetReviewById([AliasAs("id")] string reviewId)
        {
            try
            {
                return await _restClient.GetReviewById(reviewId);
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

        // Maak een nieuwe review aan
        public async Task CreateReview(CreateReview command)
        {
            await _restClient.CreateReview(command);
        }
    }
}