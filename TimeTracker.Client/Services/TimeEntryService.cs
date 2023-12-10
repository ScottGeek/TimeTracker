﻿using System.Net.Http.Json;
using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly HttpClient _http;
        public List<TimeEntryResponse> TimeEntries { get; set; } = new List<TimeEntryResponse>();
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

            if (results != null)
            { 
               TimeEntries = results;
               OnChange?.Invoke();
            }

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
    }
}
