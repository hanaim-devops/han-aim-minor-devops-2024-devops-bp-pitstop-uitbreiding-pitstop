namespace Pitstop.MaintenanceHistoryAPI.Controllers;

[Route("/api/[controller]")]
public class MaintenanceHistoryController : Controller
{
    IMessagePublisher _messagePublisher;
    MaintenanceHistoryContext _dbContext;

    public MaintenanceHistoryController(MaintenanceHistoryContext dbContext, IMessagePublisher messagePublisher)
    {
        _dbContext = dbContext;
        _messagePublisher = messagePublisher;
    }
    
    [HttpGet]
    [Route("{licenseNumber}")]
    public async Task<IActionResult> GetByLicenseNumber(string licenseNumber)
    {
        var history = await _dbContext.MaintenanceHistories.FirstOrDefaultAsync(c => c.LicenseNumber == licenseNumber);
        if (history == null)
        {
            return NotFound();
        }
        return Ok(history);
    }
}