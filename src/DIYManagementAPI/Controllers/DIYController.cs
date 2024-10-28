using DIYManagementAPI.Models;
using DIYManagementAPI.Models.DTO;
using DIYManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DIYManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DIYController : ControllerBase
    {
        private readonly DIYService _service;

        public DIYController(DIYService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<DIYEveningModel>> CreateDIYEvening([FromBody] DIYEveningCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateDIYEvening(dto);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DIYEveningModel>>> GetDIYEvening()
        {
            var result = await _service.GetDIYEvenings();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DIYEveningModel>> GetDIYEveningById(int id)
        {
            var result = await _service.GetDIYEveningById(id);
            return Ok(result);
        }
        [HttpPut("cancel/{id}")]
        public async Task<ActionResult<DIYEveningModel>> CancelDIYEvening(int id)
        {
            try
            {
                var result = await _service.CancelDIYEvening(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("registercustomer")]
        public async Task<ActionResult> RegisterDIYEveningCustomer([FromBody] DIYRegistrationCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.RegisterDIYEveningCustomer(dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("{id}/registrations")]
        public async Task<ActionResult<IEnumerable<DIYRegistration>>> GetRegistrationsForDIYEvening(int id)
        {
            var result = await _service.GetRegistrationsForDIYEvening(id);
            return Ok(result);
        }

        [HttpPost("cancelregistration/{diyRegistrationId}")]
        public async Task<ActionResult> CancelRegistration(int diyRegistrationId)
        {
            var result = await _service.CancelDIYRegistration(diyRegistrationId);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
        
        [HttpPost("registerfeedback")]
        public async Task<ActionResult<DIYEveningModel>> RegisterDIYFeedback([FromBody] DIYFeedbackCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.RegisterDIYFeedback(dto);

            return StatusCode(StatusCodes.Status201Created, null);
        }
    }
}
