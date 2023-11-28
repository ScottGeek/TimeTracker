using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared.Entities;



namespace TimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntryController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }


        [HttpGet]
        public ActionResult<List<TimeEntryResponse>> GetAllTimeEntries()
        {
            return Ok(_timeEntryService.GetAllTimeEntries());
        }

        [HttpGet("{id}")]
        public ActionResult<TimeEntryResponse> GetTimeEntryById(int id)
        {
            var result = _timeEntryService.GetTimeEntryById(id);
            if (result == null)
            {
                return NotFound("Time Entry with the given ID was not found!");
            }
            return Ok(result);
        }


        [HttpPost]
        public ActionResult<List<TimeEntryResponse>> CreateTimeEntry(TimeEntryCreateRequest timeEntry)
        {
            var result = _timeEntryService.CreateTimeEntries(timeEntry);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult<List<TimeEntryResponse>> UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry)
        {
            var result = _timeEntryService.UpdateTimeEntry(id, timeEntry);
            if (result == null)
            {
                return NotFound("Time Entry with the given ID was not found!");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<TimeEntryResponse>> DeleteTimeEntry(int id)
        {
            var result = _timeEntryService.DeleteTimeEntry(id);
            if (result == null)
            {
                return NotFound("Time Entry with the given ID was not found!");
            }
            return Ok(result);
        }
    }
}
