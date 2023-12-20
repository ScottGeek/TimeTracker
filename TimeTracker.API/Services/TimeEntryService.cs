

namespace TimeTracker.API.Services
{
    public class TimeEntryService : ITimeEntryService
    {

        private readonly ITimeEntryRepository _timeEntryRepository;

        public TimeEntryService(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }

        public async Task<List<TimeEntryResponse>> CreateTimeEntries(TimeEntryCreateRequest request)
        {
            #region Manual Mapping
            //var newEntry = new TimeEntry
            //{
            //     Project = timeEntry.Project,
            //     Start = timeEntry.Start,
            //     End = timeEntry.End
            //};
            #endregion

            var newEntry = request.Adapt<TimeEntry>();

            var result = await _timeEntryRepository.CreateTimeEntries(newEntry);

            #region Manual Mapping
            //return result.Select(t => new TimeEntryResponse
            //{
            //    Id = t.Id,
            //    Project = t.Project,
            //    Start = t.Start,
            //    End = t.End
            //}).ToList();
            #endregion

            return result.Adapt<List<TimeEntryResponse>>();

        }

        public async Task<List<TimeEntryResponse>?> DeleteTimeEntry(int id)
        {
            var result = await _timeEntryRepository.DeleteTimeEntry(id);
            if (result == null)
            {
                return null;
            }

            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<List<TimeEntryResponse>> GetAllTimeEntries()
        {
            var result = await _timeEntryRepository.GetAllTimeEntries();
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<TimeEntryResponseWrapper> GetTimeEntries(int skip, int limit)
        {
            var timeEntries = await _timeEntryRepository.GetTimeEntries(skip, limit);
            var timeEntriesResponses = timeEntries.Adapt<List<TimeEntryResponse>>();
            var timeEntriesCount = await _timeEntryRepository.GetTimeEntriesCount();
            return new TimeEntryResponseWrapper
            { 
              TimeEntries = timeEntriesResponses,
              Count = timeEntriesCount
            };
        }

        public async Task<List<TimeEntryResponse>> GetTimeEntriesByDay(int day, int month, int year)
        {
            var result = await _timeEntryRepository.GetTimeEntriesByDay(day, month, year);
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<List<TimeEntryResponse>> GetTimeEntriesByMonth(int month, int year)
        {
            var result = await _timeEntryRepository.GetTimeEntriesByMonth(month,year);
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<List<TimeEntryResponse>> GetTimeEntriesByYear(int year)
        {
            var result = await _timeEntryRepository.GetTimeEntriesByYear(year);
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<TimeEntryResponse?> GetTimeEntryById(int id)
        {
            var result = await _timeEntryRepository.GetTimeEntryById(id);
            if (result == null)
            {
                return null;
            }

            return result.Adapt<TimeEntryResponse>();
        }

        public async Task<List<TimeEntryResponse>> GetTimeEntryByProjectId(int projectId)
        {
            var result = await _timeEntryRepository.GetTimeEntriesByProject(projectId);
            return result.Adapt<List<TimeEntryResponse>>();

        }

        public async Task<List<TimeEntryResponse>?> UpdateTimeEntry(int id, TimeEntryUpdateRequest request)
        {
            try
            {
                var updatedEntry = request.Adapt<TimeEntry>();
                var result = await _timeEntryRepository.UpdateTimeEntry(id, updatedEntry);
                return result.Adapt<List<TimeEntryResponse>>();
            }
            catch (EntityNotFoundException)
            {

                return null;
            }


        }
    }
}
