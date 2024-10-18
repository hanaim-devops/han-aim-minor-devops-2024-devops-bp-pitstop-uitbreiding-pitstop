namespace WebApp.RESTClients;

public interface IDIYManagementAPI
{
    [Get("/diy")]
    Task<List<DIYEvening>> GetDIYEvening();

    //TODO: get eveninging on id

    [Get("/diyevening/{id}")]
    Task<DIYEvening> GetDIYEveningById([AliasAs("id")] string diyEveningId);

    [Post("/diy")]
    Task RegisterDIYEvening(RegisterDIYEvening cmd);
}