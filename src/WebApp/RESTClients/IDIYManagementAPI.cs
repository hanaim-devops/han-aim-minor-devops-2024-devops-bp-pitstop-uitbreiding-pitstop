namespace WebApp.RESTClients;

public interface IDIYManagementAPI
{
    [Get("/diyavonden")]
    Task<List<DIYAvond>> GetDIYAvonden();

    [Get("/diyavonden/{id}")]
    Task<DIYAvond> GetDIYAvondById([AliasAs("id")] int diyAvondId);

    //[Post("/diyavonden")]
    //Task RegisterDIYAvond(RegisterDIYAvond command);

    [Post("/diyavonden/registercustomer")]
    Task RegisterDIYAvondCustomer(RegisterDIYRegistration command);
}