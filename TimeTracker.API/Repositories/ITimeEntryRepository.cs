
namespace TimeTracker.API.Repositories
{
    public interface ITimeEntryRepository
    {

        Task<List<TimeEntry>> GetAllTimeEntries();
        Task<TimeEntry>? GetTimeEntryById(int id);
        Task<List<TimeEntry>> CreateTimeEntries(TimeEntry timeEntry);
        Task<List<TimeEntry>?> UpdateTimeEntry(int id, TimeEntry timeEntry);
        Task<List<TimeEntry>?> DeleteTimeEntry(int id);
        Task<List<TimeEntry>> GetTimeEntriesByProject(int projectId);
    }
}
