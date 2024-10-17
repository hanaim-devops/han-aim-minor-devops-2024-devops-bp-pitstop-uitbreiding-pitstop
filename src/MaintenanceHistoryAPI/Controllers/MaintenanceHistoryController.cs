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
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _dbContext.MaintenanceHistories.ToListAsync());
    }
    
    [HttpGet]
    [Route("{licenseNumber}")]
    public async Task<IActionResult> GetByLicenseNumber(string licenseNumber)
    {
        var history = await _dbContext.MaintenanceHistories
            .Where(c => c.LicenseNumber == licenseNumber)
            .ToListAsync();
    
        if (!history.Any())
        {
            return NotFound();
        }
        
        return Ok(history);
    }
}