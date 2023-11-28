using Mapster;

namespace TimeTracker.API.Services
{
    public class TimeEntryService : ITimeEntryService
    {

        private readonly ITimeEntryRepository _timeEntryRepository;

        public TimeEntryService(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }

        public List<TimeEntryResponse> CreateTimeEntries(TimeEntryCreateRequest request)
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

            var result = _timeEntryRepository.CreateTimeEntries(newEntry);

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

        public List<TimeEntryResponse>? DeleteTimeEntry(int id)
        {
            var result = _timeEntryRepository.DeleteTimeEntry(id);
            if (result == null)
            {
                return null;
            }

            return result.Adapt<List<TimeEntryResponse>>();
        }

        public List<TimeEntryResponse> GetAllTimeEntries()
        {
            var result = _timeEntryRepository.GetAllTimeEntries();
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public TimeEntryResponse? GetTimeEntryById(int id)
        {
            var result = _timeEntryRepository.GetTimeEntryById(id);
            if (result == null)
            {
                return null;
            }

            return result.Adapt<TimeEntryResponse>();
        }

        public List<TimeEntryResponse>? UpdateTimeEntry(int id, TimeEntryUpdateRequest request)
        {
            var updatedEntry = request.Adapt<TimeEntry>();
            var result = _timeEntryRepository.UpdateTimeEntry(id, updatedEntry);
            if(result == null)
            {
                return null;
            }

            return result.Adapt<List<TimeEntryResponse>>();

        }
    }
}
