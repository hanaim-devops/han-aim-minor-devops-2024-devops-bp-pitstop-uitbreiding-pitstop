using Microsoft.AspNetCore.Mvc;
using DIYManagementAPI.Models;
using DIYManagementAPI.Services;

namespace DIYManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiyTestModelsController : ControllerBase
    {
        private readonly DiyTestModelService _service;

        public DiyTestModelsController(DiyTestModelService service)
        {
            _service = service;
        }

        // GET: api/DiyTestModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiyTestModel>>> GetListings()
        {
            var results = _service.GetTestResults();
            return Ok(results);
        }
    }
}
