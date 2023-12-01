
namespace TimeTracker.API.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {


        private readonly DataContext _context;

        public TimeEntryRepository(DataContext context)
        {
            _context = context;
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
            await _context.TimeEntries.AddAsync(timeEntry);
            await _context.SaveChangesAsync();


            return await _context.TimeEntries.ToListAsync();
        }

        public async Task<List<TimeEntry>?> DeleteTimeEntry(int id)
        {
            var dbTimeEntry = await _context.TimeEntries.FindAsync(id);
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
            return await _context.TimeEntries.ToListAsync();
        }

        public async Task<TimeEntry?> GetTimeEntryById(int id)
        {

            var timeEntry = await _context.TimeEntries.FindAsync(id);
            return timeEntry;
        }

        public async Task<List<TimeEntry>?> UpdateTimeEntry(int id, TimeEntry timeEntry)
        {

            var dbTimeEntry = await _context.TimeEntries.FindAsync(id);
            if (dbTimeEntry == null)
            {
                throw new EntityNotFoundException($"Entity with ID {id} was not found.");
            }

            dbTimeEntry.Project = timeEntry.Project;

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
