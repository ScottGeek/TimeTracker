using TimeTracker.Shared.Entities;

namespace TimeTracker.API.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {

        public static List<TimeEntry> _timeEntries = new List<TimeEntry>
        {
           new TimeEntry
           {
               Id = 1,
               Project = "TimeTracker",
               Start = DateTime.Now,
               End = DateTime.Now.AddHours(1)
           }
        };

        public List<TimeEntry> CreateTimeEntries(TimeEntry timeEntry)
        {
            _timeEntries.Add(timeEntry);
            return _timeEntries;
        }

        public List<TimeEntry>? DeleteTimeEntry(int id)
        {
            var entryToDeleteIndex = _timeEntries.FirstOrDefault(x => x.Id == id);
            if (entryToDeleteIndex == null)
            {
                return null;
            }

            _timeEntries.Remove(entryToDeleteIndex);
            return _timeEntries;

        }

        public List<TimeEntry> GetAllTimeEntries()
        {
            return _timeEntries;
        }

        public TimeEntry? GetTimeEntryById(int id)
        {
            return  _timeEntries.FirstOrDefault(x => x.Id == id);
        }

        public List<TimeEntry>? UpdateTimeEntry(int id, TimeEntry timeEntry)
        {
            var entryToUpdateIndex = _timeEntries.FindIndex(x => x.Id == id);
            if (entryToUpdateIndex == -1)
            {
                return null;
            }

            _timeEntries[entryToUpdateIndex] = timeEntry;

            return _timeEntries;

        }
    }
}
