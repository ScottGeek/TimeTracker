﻿using TimeTracker.Shared.Entities;

namespace TimeTracker.API.Services
{
    public interface ITimeEntryService
    {
        Task<List<TimeEntryResponse>> GetAllTimeEntries();
        Task<TimeEntryResponseWrapper> GetTimeEntries(int skip, int limit);
        Task<TimeEntryResponse?> GetTimeEntryById(int id);
        Task<List<TimeEntryResponse>> CreateTimeEntries(TimeEntryCreateRequest request);
        Task<List<TimeEntryResponse>?> UpdateTimeEntry(int id, TimeEntryUpdateRequest request);
        Task<List<TimeEntryResponse>?> DeleteTimeEntry(int id);
        Task<List<TimeEntryResponse>> GetTimeEntryByProjectId(int projectId);

        Task<List<TimeEntryResponse>> GetTimeEntriesByYear(int year);
        Task<List<TimeEntryResponse>> GetTimeEntriesByMonth(int month, int year);
        Task<List<TimeEntryResponse>> GetTimeEntriesByDay(int day, int month, int year);
    }
}
