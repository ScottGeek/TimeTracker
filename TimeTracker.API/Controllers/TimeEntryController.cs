using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace TimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntryController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
            //more comments here

        }


        [HttpGet]
        public async Task<ActionResult<List<TimeEntryResponse>>> GetAllTimeEntries()
        {

            
            return Ok(await _timeEntryService.GetAllTimeEntries());
        }

        [HttpGet("{skip}/{limit}")]
        public async Task<ActionResult<TimeEntryResponseWrapper>> GetTimeEntries(int skip, int limit)
        {
            return Ok(await _timeEntryService.GetTimeEntries(skip,limit));
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<List<TimeEntryResponse>>> GetTimeEntriesByProject(int projectId)
        {
            return Ok(await _timeEntryService.GetTimeEntryByProjectId(projectId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeEntryResponse>> GetTimeEntryById(int id)
        {
            var result = await _timeEntryService.GetTimeEntryById(id);
            if (result == null)
            {
                return NotFound("Time Entry with the given ID was not found!");
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<List<TimeEntryResponse>>> CreateTimeEntry(TimeEntryCreateRequest timeEntry)
        {
            var result = await _timeEntryService.CreateTimeEntries(timeEntry);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<TimeEntryResponse>>> UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry)
        {
            var result = await _timeEntryService.UpdateTimeEntry(id, timeEntry);
            if (result == null)
            {
                return NotFound("Time Entry with the given ID was not found!");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TimeEntryResponse>>> DeleteTimeEntry(int id)
        {
            var result = await _timeEntryService.DeleteTimeEntry(id);
            if (result == null)
            {
                return NotFound("Time Entry with the given ID was not found!");
            }
            return Ok(result);
        }

        [HttpGet("year/{year}")]
        public async Task<ActionResult<List<TimeEntryResponse>>>GetTimeEntriesByYear(int year)
        {
            var result = await _timeEntryService.GetTimeEntriesByYear(year);
            return Ok(result);
        }

        [HttpGet("month/{month}/{year}")]
        public async Task<ActionResult<List<TimeEntryResponse>>> GetTimeEntriesByMonth(int month, int year)
        {
            var result = await _timeEntryService.GetTimeEntriesByMonth(month, year);
            return Ok(result);
        }

        [HttpGet("day/{day}/{month}/{year}")]
        public async Task<ActionResult<List<TimeEntryResponse>>> GetTimeEntriesByDay(int day, int month, int year)
        {
            var result = await _timeEntryService.GetTimeEntriesByDay(day, month, year);
            return Ok(result);
        }
    }
}
