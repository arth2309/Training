using ProjectManagementSystemServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystemServices.Interface
{
    public interface IDashBoardServices
    {

        ProjectVM GetList();

        List<ProjectList> GetListFilter(string srchName);

        Task<bool> DeleteData(int id);

        Task<bool> AddData(ProjectList projectList);

        ProjectList GetData(int id);

        Task<bool> UpdateData(ProjectList projectList);
    }
}
