using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystemRepo.Interface;
using ProjectManagementSystemRepo.DataModels;
using ProjectManagementSystemServices.ModelView;
using ProjectManagementSystemServices.Interface;


namespace ProjectManagementSystemServices.Implementation
{
    public class DashBoardServices : IDashBoardServices
    {
        private readonly IProjectRepo _projectRepo;

        public DashBoardServices(IProjectRepo projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public ProjectVM GetList()
        {
            ProjectVM vm = new();
            List<ProjectList> lists = new List<ProjectList>();

            List<Project> projects = _projectRepo.GetList();

            for(int i = 0; i < projects.Count; i++) 
            {
                ProjectList projectList = new();
                projectList.ProjectId = projects[i].Id;
                projectList.ProjectDescription = projects[i].Description;
                projectList.DueDate = projects[i].DueDate;
                projectList.Assignee = projects[i].Assignee;
                projectList.ProjectName = projects[i].ProjectName;
                projectList.DomainName = projects[i].DomainNavigation.Name;
                projectList.City = projects[i].City;
                lists.Add(projectList);
            }

            vm.lists = lists;

            return vm;
        }

        public List<ProjectList> GetListFilter(string srchName)
        {
            
            List<ProjectList> lists = new List<ProjectList>();

            List<Project> projects = _projectRepo.GetList(srchName);

            for (int i = 0; i < projects.Count; i++)
            {
                ProjectList projectList = new();
                projectList.ProjectId = projects[i].Id;
                projectList.ProjectDescription = projects[i].Description;
                projectList.DueDate = projects[i].DueDate;
                projectList.Assignee = projects[i].Assignee;
                projectList.ProjectName = projects[i].ProjectName;
                projectList.DomainName = projects[i].DomainNavigation.Name;
                projectList.City = projects[i].City;
                lists.Add(projectList);
            }

          

            return lists;
        }

        public async Task<bool> DeleteData(int id)
        {
            Project project = _projectRepo.GetData(id);
            bool result = await _projectRepo.RemoveData(project);
            return result;
        }

        public async Task<bool> AddData(ProjectList projectList)
        {
            Project project = new();
            project.DomainId = 1;
            project.ProjectName = projectList.ProjectName;
            project.CreatedDate = DateTime.Now;
            project.DueDate = projectList.DueDate;
            project.Description = projectList.ProjectDescription;
            project.Domain = "Not required";
            project.Assignee = projectList.Assignee;
            project.City = projectList.City;

            bool result = await _projectRepo.AddData(project);
            return result;
        }


        public ProjectList GetData(int id)
        {
            Project project = _projectRepo.GetData(id);
            ProjectList projectList = new ProjectList();
            projectList.ProjectId = project.Id;
            projectList.ProjectDescription = project.Description;
            projectList.DueDate = project.DueDate;
            projectList.Assignee = project.Assignee;
            projectList.ProjectName = project.ProjectName;
            projectList.City = project.City;

            return projectList;


        }

        public async Task<bool> UpdateData(ProjectList projectList)
        {
            Project project = _projectRepo.GetData(projectList.ProjectId);
            project.ProjectName = projectList.ProjectName;
            project.CreatedDate = DateTime.Now;
            project.DueDate = projectList.DueDate;
            project.Description = projectList.ProjectDescription;
            project.Assignee = projectList.Assignee;
            project.City = projectList.City;

            bool result = await _projectRepo.UpDateData(project);
            return result;
        }






    }
}
