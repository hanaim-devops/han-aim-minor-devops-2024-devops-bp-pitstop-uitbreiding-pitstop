namespace WebApp.RESTClients;

public interface IDIYManagementAPI
{
    [Get("/diy")]
    Task<List<DIYEvening>> GetDIYEvening();

    [Post("/diy/registercustomer")]
    Task RegisterDIYEveningCustomer(RegisterDIYRegistration command);

    [Get("/diy/{id}/registrations")]
    Task<List<DIYRegistration>> GetRegistrationsForDIYEvening([AliasAs("id")] string diyEveningId);

    [Get("/diy/{id}")]
    Task<DIYEvening> GetDIYEveningById([AliasAs("id")] string diyEveningId);

    [Post("/diy")]
    Task RegisterDIYEvening(RegisterDIYEvening cmd);

    [Post("/diy/registerfeedback")]
    Task RegisterDIYFeedback(RegisterDIYFeedback cmd);
}