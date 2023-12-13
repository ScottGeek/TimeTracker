
namespace TimeTracker.API.Repositories
{
    public interface IProjectRepository
    {

        Task<List<Project>> GetAllProjectEntries();
        Task<Project?> GetProjectById(int id);
        Task<List<Project>> CreateProjectEntries(Project project);
        Task<List<Project>?> UpdateProject(int id, Project project);
        Task<List<Project>?> DeleteProject(int id);
    }
}
