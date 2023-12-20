using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services
{
    public interface ITimeEntryService
    {
        event Action? OnChange;
        public List<TimeEntryResponse> TimeEntries { get; set; }
        public TimeSpan TotalDuration { get; set; }
        public Task GetTimeEntriesByProject(int projectId);
        public Task<TimeEntryResponseWrapper> GetTimeEntries(int skip, int limit);
        public Task<TimeEntryResponse> GetTimeEntryById(int id);
        public Task CreateTimeEntry(TimeEntryRequest request);
        public Task UpdateTimeEntry(int id, TimeEntryRequest request);
        public Task DeleteTimeEntry(int id);

        public Task GetTimeEntriesByYear(int year);
        public Task GetTimeEntriesByMonth(int month, int year);
        public Task GetTimeEntriesByDay(int day, int month, int year);

        public TimeSpan CalculateDuration(TimeEntryResponse timeEntry);

    }
}
