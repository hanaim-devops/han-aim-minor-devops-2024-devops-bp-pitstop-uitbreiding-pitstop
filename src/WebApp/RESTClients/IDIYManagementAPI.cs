namespace WebApp.RESTClients;

public interface IDIYManagementAPI
{
    [Get("/diyavonden")]
    Task<List<DIYAvond>> GetDIYAvonden();

    [Get("/diyavonden/{id}")]
    Task<DIYAvond> GetDIYAvondById([AliasAs("id")] int diyEveningId);

    [Post("/diyavonden/registercustomer")]
    Task RegisterDIYAvondCustomer(RegisterDIYRegistration command);
}