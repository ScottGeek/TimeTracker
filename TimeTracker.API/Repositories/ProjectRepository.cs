
namespace TimeTracker.API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {


        private readonly DataContext _context;

        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        //public static List<project> _timeEntries = new List<project>
        //{
        //   new project
        //   {
        //       Id = 1,
        //       Project = "TimeTracker",
        //       Start = DateTime.Now,
        //       End = DateTime.Now.AddHours(1)
        //   }
        //};

        public async Task<List<Project>> CreateProjectEntries(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();


            return await GetAllProjectEntries();
        }

        public async Task<List<Project>?> DeleteProject(int id)
        {
            var dbproject = await _context.Projects.FindAsync(id);
            if (dbproject == null)
            {
                return null;
            }

            dbproject.IsDeleted = true;
            dbproject.DateDeleted = DateTime.Now;

            await _context.SaveChangesAsync();

            return await GetAllProjectEntries();

        }

        public async Task<List<Project>> GetAllProjectEntries()
        {
            return await _context.Projects
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectById(int id)
        {

            var project = await _context.Projects
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.Id == id);
            return project;
        }

        public async Task<List<Project>?> UpdateProject(int id, Project project)
        {

            var dbProject = await _context.Projects.FindAsync(id);
            if (dbProject == null)
            {
                throw new EntityNotFoundException($"Entity with ID {id} was not found.");
            }

            if (project.ProjectDetails != null && dbProject.ProjectDetails != null)
            {
                dbProject.ProjectDetails.Description = project.ProjectDetails.Description;
                dbProject.ProjectDetails.StartDate = project.ProjectDetails.StartDate;
                dbProject.ProjectDetails.EndDate = project.ProjectDetails.EndDate;
            }
            else
            {
                if (project.ProjectDetails != null && dbProject.ProjectDetails == null)
                {
                    dbProject.ProjectDetails = new ProjectDetails
                    {
                        Description = project.ProjectDetails.Description,
                        StartDate = project.ProjectDetails.StartDate,
                        EndDate = project.ProjectDetails.EndDate,
                        Project = project
                    };
                }
            }

            dbProject.Name = project.Name;
            dbProject.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            return await GetAllProjectEntries();

        }
    }
}
