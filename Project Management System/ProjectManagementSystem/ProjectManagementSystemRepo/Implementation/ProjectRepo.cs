using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystemRepo.DataContext;
using ProjectManagementSystemRepo.DataModels;
using ProjectManagementSystemRepo.Interface;

namespace ProjectManagementSystemRepo.Implementation
{
    public class ProjectRepo : IProjectRepo
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Project> GetList(string name = null)
        {
            if (name == null)
            {
                return _context.Projects.Include(a => a.DomainNavigation).ToList();
            }
            else
            {
                return _context.Projects.Include(a => a.DomainNavigation).Where(a => a.ProjectName.Contains(name) || a.Assignee.Contains(name) || a.Description.Contains(name)).ToList();
            }
        }

        public Project GetData(int id)
        {
            return _context.Projects.FirstOrDefault(a => a.Id == id);
        }

        public async Task<bool> RemoveData(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddData(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpDateData(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }


}
