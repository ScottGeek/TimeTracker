
namespace TimeTracker.API.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {


        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public TimeEntryRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        //public static List<TimeEntry> _timeEntries = new List<TimeEntry>
        //{
        //   new TimeEntry
        //   {
        //       Id = 1,
        //       Project = "TimeTracker",
        //       Start = DateTime.Now,
        //       End = DateTime.Now.AddHours(1)
        //   }
        //};

        public async Task<List<TimeEntry>> CreateTimeEntries(TimeEntry timeEntry)
        {

            var user = await _userContextService.GetUserAsync();
            if(user == null)
            {
                throw new EntityNotFoundException("User not found!");
            }

            timeEntry.User = user;

            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();


            return await GetAllTimeEntries();
        }

        public async Task<List<TimeEntry>?> DeleteTimeEntry(int id)
        {

            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                return null;
            }

            var dbTimeEntry = await _context.TimeEntries
                      .FirstOrDefaultAsync(t => t.Id == id && t.User.Id == userId);                
              
            if (dbTimeEntry == null)
            {
                return null;
            }

            _context.TimeEntries.Remove(dbTimeEntry);
            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();

        }

        public async Task<List<TimeEntry>> GetAllTimeEntries()
        {

            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                return new List<TimeEntry>();
            }

            return await _context.TimeEntries
                .Where(t => t.User.Id == userId)
                .ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntries(int skip, int limit)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                return new List<TimeEntry>();
            }


            return await _context.TimeEntries
                .Where(t => t.User.Id == userId)
                .Skip(skip)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesByProject(int projectId)
        {

            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                return new List<TimeEntry>();
            }

            var result = await _context.TimeEntries
                .Where(t => t.ProjectId == projectId && t.User.Id == userId)
                .ToListAsync();
            return result;
        }

        public async Task<int> GetTimeEntriesCount()
        {

            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                return (int)0;
            }

            return await _context.TimeEntries
                .Where(t => t.User.Id == userId)
                .CountAsync();
        }

        public async Task<TimeEntry?> GetTimeEntryById(int id)
        {

            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                return null;
            }

            var timeEntry = await _context.TimeEntries
                .FirstOrDefaultAsync(t => t.Id == id && t.User.Id == userId);
            return timeEntry;
        }

        public async Task<List<TimeEntry>?> UpdateTimeEntry(int id, TimeEntry timeEntry)
        {

            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                throw new EntityNotFoundException("User not found!");
            }

            var dbTimeEntry = await _context.TimeEntries
                .FirstOrDefaultAsync(t => t.Id == id && t.User.Id == userId); ;
            if (dbTimeEntry == null)
            {
                throw new EntityNotFoundException($"Entity with ID {id} was not found.");
            }

            dbTimeEntry.ProjectId = timeEntry.ProjectId;

            dbTimeEntry.Start = timeEntry.Start;
            if (timeEntry.End != null)
            {
                dbTimeEntry.End = timeEntry.End;
            }

            //dbTimeEntry.Start = timeEntry.Start;
            //dbTimeEntry.End = timeEntry.End;

            dbTimeEntry.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();

        }
    }
}
