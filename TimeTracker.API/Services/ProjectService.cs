

namespace TimeTracker.API.Services
{
    public class ProjectService : IProjectService
    {

        private readonly IProjectRepository _projectyRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectyRepository = projectRepository;
        }

        public async Task<List<ProjectResponse>> CreateProject(ProjectCreateRequest request)
        {
            #region Manual Mapping
            //var newEntry = new TimeEntry
            //{
            //     Project = timeEntry.Project,
            //     Start = timeEntry.Start,
            //     End = timeEntry.End
            //};
            #endregion

            var newEntry = request.Adapt<Project>();
            newEntry.ProjectDetails = request.Adapt<ProjectDetails>();

            var result = await _projectyRepository.CreateProjectEntries(newEntry);

            #region Manual Mapping
            //return result.Select(t => new TimeEntryResponse
            //{
            //    Id = t.Id,
            //    Project = t.Project,
            //    Start = t.Start,
            //    End = t.End
            //}).ToList();
            #endregion

            return result.Adapt<List<ProjectResponse>>();

        }

        public async Task<List<ProjectResponse>?> DeleteProject(int id)
        {
            var result = await _projectyRepository.DeleteProject(id);
            if (result == null)
            {
                return null;
            }

            return result.Adapt<List<ProjectResponse>>();
        }

        public async Task<List<ProjectResponse>> GetAllProjects()
        {
            var result = await _projectyRepository.GetAllProjectEntries();
            return result.Adapt<List<ProjectResponse>>();
        }

        public async Task<ProjectResponse?> GetProjectById(int id)
        {
            var result = await _projectyRepository.GetProjectById(id);
            if (result == null)
            {
                return null;
            }

            return result.Adapt<ProjectResponse>();
        }

        public async Task<List<ProjectResponse>?> UpdateProject(int id, ProjectUpdateRequest request)
        {
            try
            {
                var updatedEntry = request.Adapt<Project>();
                updatedEntry.ProjectDetails = request.Adapt<ProjectDetails>();
                var result = await _projectyRepository.UpdateProject(id, updatedEntry);
                return result.Adapt<List<ProjectResponse>>();
            }
            catch (EntityNotFoundException)
            {

                return null;
            }


        }
    }
}
