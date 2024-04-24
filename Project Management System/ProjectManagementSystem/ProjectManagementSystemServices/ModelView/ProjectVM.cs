using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystemServices.ModelView;

namespace ProjectManagementSystemServices.ModelView
{
    public class ProjectVM
    {
       public  List<ProjectList>? lists { get; set; }

        public ProjectList? list { get; set; }
    } 
}
