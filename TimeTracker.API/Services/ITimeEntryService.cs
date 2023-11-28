using TimeTracker.Shared.Entities;

namespace TimeTracker.API.Services
{
    public interface ITimeEntryService
    {
        List<TimeEntryResponse> GetAllTimeEntries();
        TimeEntryResponse? GetTimeEntryById(int id);
        List<TimeEntryResponse> CreateTimeEntries(TimeEntryCreateRequest request);
        List<TimeEntryResponse>? UpdateTimeEntry(int id, TimeEntryUpdateRequest request);
        List<TimeEntryResponse>? DeleteTimeEntry(int id);
    }
}
