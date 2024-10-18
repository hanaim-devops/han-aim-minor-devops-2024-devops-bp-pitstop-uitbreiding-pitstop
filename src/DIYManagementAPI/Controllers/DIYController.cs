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
        private readonly DYIService _service;

        public DIYController(DYIService service)
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

            var diyEvening = new DIYEveningModel
            {
                Title = dto.Title,
                ExtraInfo = dto.ExtraInfo,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Mechanic = dto.Mechanic
            };

            var result = await _service.CreateDIYEvening(diyEvening);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPost("registerfeedback")]
        public async Task<ActionResult<DIYEveningModel>> CreateDIYFeedback([FromBody] DIYFeedbackCreateDto dto)
        {
            Console.WriteLine($"Received feedback: DIYEveningId = {dto.DIYEveningId}, CustomerName = {dto.CustomerName}, Feedback = {dto.Feedback}");
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var feedback = new DIYFeedback
            {
                DIYEveningID = dto.DIYEveningId,
                CustomerName = dto.CustomerName,
                Feedback = dto.Feedback
            };

            await _service.RegisterDIYFeedback(feedback);

            return StatusCode(StatusCodes.Status201Created, null);
        }

        // TODO: get all DIYEveningModels
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
        // TODO: Annuleer meeting
        // TODO: meld aan als klant
        [HttpPost("registercustomer")]
        public async Task<ActionResult> RegisterDIYEveningCustomer([FromBody] DIYRegistrationCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registration = new DIYRegistration
            {
                DIYEveningID = dto.DIYEveningId,
                CustomerName = dto.CustomerName,
                Reparations = dto.Reparations
            };

            await _service.RegisterDIYEveningCustomer(registration);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
