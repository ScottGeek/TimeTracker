using TimeTracker.Shared.Entities;

namespace TimeTracker.API.Repositories
{
    public interface ITimeEntryRepository
    {

        List<TimeEntry> GetAllTimeEntries();
        TimeEntry? GetTimeEntryById(int id);
        List<TimeEntry> CreateTimeEntries(TimeEntry timeEntry);
        List<TimeEntry>? UpdateTimeEntry(int id, TimeEntry timeEntry);
        List<TimeEntry>? DeleteTimeEntry(int id);
    }
}
