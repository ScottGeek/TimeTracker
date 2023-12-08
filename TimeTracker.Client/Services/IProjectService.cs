
namespace TimeTracker.Client.Services
{
    public interface IProjectService
    {
        public List<ProjectResponse> Projects { get; set; }

        public event Action? OnChange;

        public Task LoadAllProjects();

        public Task<ProjectResponse> GetProjectById(int id);

        public Task CreateProject(ProjectRequest request);

        public Task UpdateProject(int id, ProjectRequest request);

        public Task DeleteProject(int id);

    }
}
