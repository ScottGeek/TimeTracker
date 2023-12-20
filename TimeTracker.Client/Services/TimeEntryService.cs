using System.Net.Http.Json;
using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly HttpClient _http;
        public List<TimeEntryResponse> TimeEntries { get; set; } = new List<TimeEntryResponse>();
        public TimeSpan TotalDuration { get; set; }

        public event Action? OnChange;

        public TimeEntryService(HttpClient http)
        {
            _http = http;
        }


        public async Task GetTimeEntriesByProject(int projectId)
        {
            List<TimeEntryResponse>? results = null;

            if (projectId <= 0)
            {
                results = await _http.GetFromJsonAsync<List<TimeEntryResponse>>("api/timeentry");
            }
            else
            {
                results = await _http.GetFromJsonAsync<List<TimeEntryResponse>>($"api/timeentry/project/{projectId}");
            }

            SetTimeEntries(results);

        }


        public async Task<TimeEntryResponse> GetTimeEntryById(int id)
        {
            return await _http.GetFromJsonAsync<TimeEntryResponse>($"api/timeentry/{id}");
        }

        public async Task CreateTimeEntry(TimeEntryRequest request)
        {
            await _http.PostAsJsonAsync("api/timeentry", request.Adapt<TimeEntryCreateRequest>());
        }

        public async Task UpdateTimeEntry(int id, TimeEntryRequest request)
        {
            await _http.PutAsJsonAsync($"api/timeentry/{id}", request.Adapt<TimeEntryUpdateRequest>());
        }

        public async Task DeleteTimeEntry(int id)
        {
            await _http.DeleteAsync($"api/timeentry/{id}");
        }

        public async Task<TimeEntryResponseWrapper> GetTimeEntries(int skip, int limit)
        {
            return await _http.GetFromJsonAsync<TimeEntryResponseWrapper>($"api/timeentry/{skip}/{limit}");
        }

        public async Task GetTimeEntriesByYear(int year)
        {
            var results = await _http.GetFromJsonAsync<List<TimeEntryResponse>>($"api/timeentry/year/{year}");
            SetTimeEntries(results);
        }

        public async Task GetTimeEntriesByMonth(int month, int year)
        {
            var results = await _http.GetFromJsonAsync<List<TimeEntryResponse>>($"api/timeentry/month/{month}/{year}");
            SetTimeEntries(results);
        }

        public async Task GetTimeEntriesByDay(int day, int month, int year)
        {
            var results = await _http.GetFromJsonAsync<List<TimeEntryResponse>>($"api/timeentry/day/{day}/{month}/{year}");
            SetTimeEntries(results);
        }
        private void SetTimeEntries(List<TimeEntryResponse>? results)
        {
            if (results != null)
            {
                TimeEntries = results;
                CalculateTotalDuration();
                OnChange?.Invoke();
            }
        }

        public TimeSpan CalculateDuration(TimeEntryResponse timeEntry)
        {

            if (timeEntry.End == null || timeEntry.End.Value < timeEntry.Start)
            {
                return new TimeSpan();
            }

            TimeSpan duration = timeEntry.End.Value - timeEntry.Start;
            return duration;

        }

        private void CalculateTotalDuration()
        {
            TotalDuration = new TimeSpan();
            foreach (var timeEntry in TimeEntries)
            {
                TotalDuration += CalculateDuration(timeEntry);
            }
        }
    }
}
