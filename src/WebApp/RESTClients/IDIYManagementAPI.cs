namespace WebApp.RESTClients;

public interface IDIYManagementAPI
{
    [Get("/diy")]
    Task<List<DIYEvening>> GetDIYEvening();

    [Post("/diyavonden/registercustomer")]
    Task RegisterDIYAvondCustomer(RegisterDIYRegistration command);
    //TODO: get eveninging on id

    [Get("/diyevening/{id}")]
    Task<DIYEvening> GetDIYEveningById([AliasAs("id")] string diyEveningId);

    [Post("/diy")]
    Task RegisterDIYEvening(RegisterDIYEvening cmd);

    [Post("/diy")]
    Task RegisterDIYFeedback(RegisterDIYFeedback cmd);
}