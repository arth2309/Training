using ProjectManagementSystemRepo.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystemRepo.Interface
{
    public interface IProjectRepo
    {
        List<Project> GetList(string name);
        Project GetData(int id);
        Task<bool> RemoveData(Project project);

        Task<bool> AddData(Project project);

        Task<bool> UpDateData(Project project);
    }
}
